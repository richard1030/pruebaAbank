using LoginService.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILoginRepository _clienteRepo;
        public ClientesController(ILoginRepository loginRepo)
        {
            _clienteRepo = loginRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                var companies = await _clienteRepo.GetClientes();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
    
}
