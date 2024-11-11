using Dapper;
using LoginService.Context;
using LoginService.Contracts;
using LoginService.Dto;
using LoginService.Models;
using System.Data;

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
        public async Task<Cliente> GetCliente(int id)
        {
            var query = "SELECT * FROM Clientes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var cliente = await connection.QuerySingleOrDefaultAsync<Cliente>(query, new { id });

                return cliente;
            }
        }

        public async Task<Cliente> CreateCliente(ClienteForCreationDto cliente)
        {
            var query = "INSERT INTO Clientes (nombres, apellidos, fechanacimiento,direccion,password,telefono,email,fechacreacion) VALUES (@nombres, @apellidos, @fechanacimiento,@direccion,@password,@telefono,@email,@fechacreacion)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("nombres", cliente.nombres, DbType.String);
            parameters.Add("apellidos", cliente.apellidos, DbType.String);
            parameters.Add("fechanacimiento", cliente.fechanacimiento, DbType.Date);
            parameters.Add("direccion", cliente.direccion, DbType.String);
            parameters.Add("password", cliente.password, DbType.String);
            parameters.Add("telefono", cliente.telefono, DbType.String);
            parameters.Add("email", cliente.email, DbType.String);
            parameters.Add("fechacreacion", cliente.fechacreacion, DbType.Date);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdCliente = new Cliente
                {
                    id = id,
                    nombres = cliente.nombres,
                    apellidos = cliente.apellidos,
                    direccion = cliente.direccion,
                    password = cliente.password,
                    telefono = cliente.telefono,
                    email = cliente.email

                };
                return createdCliente;
            }
        }
        public async Task UpdateCliente(int id, ClienteForUpdateDto cliente)
        {
            var query = "UPDATE Clientes SET nombres = @nombres, apellidos = @apellidos, fechanacimiento = @fechanacimiento, @direccion = direccion, @password=password, @telefono=telefono, @email = email, @fechamodificacion=fechamodificacion  WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("nombres", cliente.nombres, DbType.String);
            parameters.Add("apellidos", cliente.apellidos, DbType.String);
            parameters.Add("fechanacimiento", cliente.fechanacimiento, DbType.Date);
            parameters.Add("direccion", cliente.direccion, DbType.String);
            parameters.Add("password", cliente.password, DbType.String);
            parameters.Add("telefono", cliente.telefono, DbType.String);
            parameters.Add("email", cliente.email, DbType.String);
            parameters.Add("fechamodificacion", cliente.fechamodificacion, DbType.Date);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteCliente(int id)
        {
            var query = "DELETE FROM Clientes WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Cliente> LoginCliente(ClienteForLoginDto clienteparam)
        {
            var query = "SELECT id FROM Clientes WHERE password = @password and telefono=@telefono";
            var parameters = new DynamicParameters();
            parameters.Add("password", clienteparam.password, DbType.String);
            parameters.Add("telefono", clienteparam.telefono, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var cliente = await connection.QuerySingleOrDefaultAsync<Cliente>(query, parameters);
                return cliente;
            }
        }
    }
}
