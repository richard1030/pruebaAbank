using LoginService.Contracts;
using LoginService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILoginRepository _clienteRepo;
        public ClientesController(ILoginRepository loginRepo)
        {
            _clienteRepo = loginRepo;
        }
        [HttpGet("Users")]
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

        [HttpGet("GetUser/{id}", Name = "ClienteById")]
        public async Task<IActionResult> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteRepo.GetCliente(id);
                if (cliente == null)
                    return NotFound();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateCliente(ClienteForCreationDto cliente)
        {
            try
            {
                var createdCliente = await _clienteRepo.CreateCliente(cliente);
                return CreatedAtRoute("clienteById", new { id = createdCliente.id }, createdCliente);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

  
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteForUpdateDto cliente)
        {
            try
            {
                var dbCompany = await _clienteRepo.GetCliente(id);
                if (dbCompany == null)
                    return NotFound();
                await _clienteRepo.UpdateCliente(id, cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _clienteRepo.GetCliente(id);
                if (dbCompany == null)
                    return NotFound();
                await _clienteRepo.DeleteCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
    
}
