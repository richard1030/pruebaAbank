using LoginService.Models;

namespace LoginService.Contracts
{
    public interface ILoginRepository
    {
        public Task<IEnumerable<Cliente>> GetClientes();
    }
}
