using Dapper.Contrib.Extensions;
using DapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;

namespace DapperWith.NET.Controllers
{
    public class DapperContribController : Controller
    {
        private IDbConnection db;

        public DapperContribController(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var sql = db.GetAll<Product>().ToList();
            return View(sql);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var sql = db.Get<Product>(id);
            return View(sql);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            var eid = db.Insert(product);
            product.Id = (int)eid;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sql = db.Get<Product>(id);
            return View(sql);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var sql = db.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            db.Delete(new Product { Id = id });
            return RedirectToAction("Index");
        }
    }
}