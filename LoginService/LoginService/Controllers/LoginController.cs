using LoginService.Contracts;
using LoginService.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoginService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private IConfiguration _config;
        private readonly ILoginRepository _clienteRepo;
        public LoginController(ILoginRepository loginRepo, IConfiguration config)
        {
            _clienteRepo = loginRepo;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(ClienteForLoginDto clienteParam)
        {
            try
            {
                var cliente = await _clienteRepo.LoginCliente(clienteParam);

               if (cliente != null) {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                      _config["Jwt:Issuer"],
                      null,
                      expires: DateTime.Now.AddMinutes(120),
                      signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                    return Ok(token);
               }else
                {
                    return BadRequest("user not found");
                }

            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

           
        }
    }
}
