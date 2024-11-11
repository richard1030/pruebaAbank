using LoginService.Dto;
using LoginService.Models;

namespace LoginService.Contracts
{
    public interface ILoginRepository
    {
        public Task<IEnumerable<Cliente>> GetClientes();
        public Task<Cliente> GetCliente(int id);
        public Task<Cliente> CreateCliente(ClienteForCreationDto cliente);
        public Task UpdateCliente(int id, ClienteForUpdateDto cliente);
        public Task DeleteCliente(int id);

        public Task<Cliente> LoginCliente(ClienteForLoginDto cliente);
    }
}
