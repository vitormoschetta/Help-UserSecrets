using System.Collections.Generic;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Segredos.Models;

namespace Segredos.Controllers
{
    public class UserDapperController : Controller
    {
        private IConfiguration _configuration { get; }
        public UserDapperController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionStrings()
        {
            return "Data/DataBase.db";
        }

        public IActionResult Index()
        {
            ViewBag.Products = GetAll();
            return View();
        }

        public IEnumerable<Product> GetAll()
        {
            using var dapperConnection = new SqliteConnection(ConnectionStrings());
            return dapperConnection.Query<Product>(@"SELECT * FROM PRODUCT");
        }
    }
}