using Dapper;
using LoginService.Context;
using LoginService.Contracts;
using LoginService.Models;

namespace LoginService.Repository
{
    public class LoginRepository:ILoginRepository
    {
        private readonly DapperContext _context;
        public LoginRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            var query = "SELECT * FROM Clientes";

            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Cliente>(query);
                return companies.ToList();
            }
        }
    }
}
