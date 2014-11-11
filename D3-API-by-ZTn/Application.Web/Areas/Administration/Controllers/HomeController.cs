using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Navigation()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Car",
                    Price = 100000
                },
                new Product
                {
                    Id = 2,
                    Name = "Chair",
                    Price = 100
                },
                new Product
                {
                    Id = 3,
                    Name = "Computer",
                    Price = 1000
                }
            };
            return View(products);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}