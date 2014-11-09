using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class HomeController : BaseController
    {
        // This request is cached for 1 hour!!!
        //[OutputCache(Duration=60*60)]
        public ActionResult Index()
        {
            return View();
        }
    }
}