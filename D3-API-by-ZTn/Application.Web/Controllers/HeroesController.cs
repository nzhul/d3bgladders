using Application.Web.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class HeroesController : BaseController
    {
        // GET: Heroes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadAllHeroes([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderByDescending(x => x.Stats.Damage)
                .ThenBy(x => x.Stats.Toughness)
                .Select(x => new HeroViewModel
            {
                ID = x.ID,
                BattleTag = x.ApplicationUser.BattleTag,
                Damage = x.Stats.Damage,
                Healing = x.Stats.Healing,
                HeroClass = x.HeroClass,
                IsHardcore = x.Hardcore,
                Life = x.Stats.Life,
                Name = x.Name,
                ParagonLevel = x.ParagonLevel,
                Toughness = x.Stats.Toughness

            });

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var viewModel = this.Data.Heroes.Find(id);
            return View(viewModel);
        }
    }
}