using Application.Web.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Application.Web.Controllers
{
    public class PlayersController : BaseController
    {
        // GET: Players
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadAllCareers([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Careers.All().Select(x => new CareerSummaryViewModel
            {
                Id = x.ID,
                BattleTag = x.BattleTag,
                EliteMonsterKills = x.Kills.elites,
                HardcoreMonsterKills = x.Kills.hardcoreMonsters,
                MonsterKills = x.Kills.monsters,
                ParagonLevel = x.ParagonLevel,
                ParagonLevelSeason = x.ParagonLevelSeason,
                ParagonLevelSeasonHardcore = x.ParagonLevelSeasonHardcore,
                PostCount = x.ApplicationUser.PostsCount,
                UserName = x.ApplicationUser.UserName,
                Heroes = x.Heroes.Select(h => new HeroViewModel 
                    { 
                        ID = h.ID,
                        HeroClass = h.HeroClass,
                        Name = h.Name
                    })
            });

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var viewModel = this.Data.Careers.All()
                .Where(x => x.ID == id)
                .Select(x => new CareerDetailsViewModel
                {
                    ApplicationUser = x.ApplicationUser,
                    BattleTag = x.BattleTag,
                    Heroes = x.Heroes
                        .OrderByDescending(h => h.Stats.Damage)
                        .ThenByDescending(h => h.Stats.Toughness)
                        .Select(h => new HeroViewModel
                        {
                            ID = h.ID,
                            HeroClass = h.HeroClass,
                            Name = h.Name,
                            Damage = h.Stats.Damage,
                            Toughness = h.Stats.Toughness,
                            Life = h.Stats.Life,
                            Healing = h.Stats.Healing,
                            IsHardcore = h.Hardcore
                        }),
                    Kills = x.Kills,
                    ParagonLevel = x.ParagonLevel,
                    ParagonLevelHardcore = x.ParagonLevelHardcore,
                    ParagonLevelSeason = x.ParagonLevelSeason,
                    ParagonLevelSeasonHardcore = x.ParagonLevelSeason,
                    Progression = x.Progression,
                    TimePlayed = x.TimePlayed
                }).FirstOrDefault();

            return View(viewModel);
        }
    }
}