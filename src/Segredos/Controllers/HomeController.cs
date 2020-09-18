using System.Collections.Generic;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Segredos.Models;

namespace Segredos.Controllers
{
    public class HomeController
    {
        private IConfiguration _configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionStrings()
        {
            var builder = new SqlConnectionStringBuilder(
                    _configuration.GetSection("MyDataBaseConn").GetSection("ConnectionStrings").Value);

            builder.Password = _configuration["DatabasePassword"];  // => Recuperando o segredo 
            string connection = builder.ConnectionString;
            return connection;
        }

        public IEnumerable<Usuario> GetAll()
        {
            using (var _dapper = new SqlConnection(ConnectionStrings()))
            {
                var query = "SELECT * FROM ENTIDADE";
                return _dapper.Query<Usuario>(query);
            }
        }
    }
}