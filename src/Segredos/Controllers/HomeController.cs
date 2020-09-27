using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Segredos.Controllers
{
    public class HomeController : Controller
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Segredo()
        {
            ViewBag.ConnectionString = ConnectionStrings();
            return View();
        }

    }
}