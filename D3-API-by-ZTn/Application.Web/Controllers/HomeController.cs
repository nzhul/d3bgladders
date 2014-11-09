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
            var bestHeroes = this.Data.Heroes.All()
                .OrderByDescending(x => x.ParagonLevel)
                .Select(x => new HeroViewModel
                {
                    BattleTag = x.ApplicationUser.BattleTag,
                    Damage = x.Stats.Damage,
                    HeroClass = x.HeroClass,
                    Name = x.Name,
                    ParagonLevel = x.ParagonLevel
                }).ToList();
            return View(bestHeroes);
        }
    }
}