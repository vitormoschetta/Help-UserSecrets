using System.Collections.Generic;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Segredos.Models;

namespace Segredos.Controllers
{
    public class UserSecretsController : Controller
    {
        private IConfiguration _configuration { get; }
        public UserSecretsController(IConfiguration configuration)
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

        public IActionResult Index()
        {
            ViewBag.ConnectionString = ConnectionStrings();
            return View();
        }

    }
}