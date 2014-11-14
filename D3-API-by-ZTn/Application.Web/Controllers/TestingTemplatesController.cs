using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class TestingTemplatesController : Controller
    {
        // GET: TestingTemplates
        public ActionResult Index()
        {
            var heroList = new List<HeroViewModel>()
            {
                new HeroViewModel 
                {
                    ParagonLevel = 111,
                    Damage = 333
                },
                new HeroViewModel 
                {
                    ParagonLevel = 50,
                    Damage = 456
                },

            };
            return View(heroList);
        }
    }
}