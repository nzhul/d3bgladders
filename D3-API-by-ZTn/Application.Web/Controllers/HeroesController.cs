using Application.Web.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Application.Models.Heroes;

namespace Application.Web.Controllers
{
    public class HeroesController : BaseController
    {
        // GET: Heroes
        public ActionResult Index()
        {
            return View();
        }
        
        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroes([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x =>x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x =>x.ParagonLevel)
                .Where(x => x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesBarbarian([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Barbarian && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesBarbarianHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Barbarian && x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesWizard([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Wizard && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesWizardHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Wizard && x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesDemonHunter([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.DemonHunter && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesDemonHunterHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.DemonHunter && x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesMonk([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Monk && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesMonkHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Monk && x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesWitchDoctor([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.WitchDoctor && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesWitchDoctorHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.WitchDoctor && x.Hardcore == true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesCrusader([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Crusader && x.Hardcore != true)
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

        //[OutputCache(Duration = 60 * 60)]
        public JsonResult ReadAllHeroesCrusaderHardcore([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.Data.Heroes.All()
                .OrderBy(x => x.ParagonLevel)
                .Where(x => x.HeroClass == HeroClass.Crusader && x.Hardcore == true)
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
            var currentUserId = this.User.Identity.GetUserId();
            var viewModel = this.Data.Heroes.Find(id);
            ViewBag.CanVote = !this.Data.HeroVotes.All().Any(x => x.HeroId == id && x.VotedById == currentUserId);
            return View(viewModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                var currentUser = this.Data.Users.Find(currentUserId);

                this.Data.HeroComments.Add(new HeroComment()
                {
                    AuthorId = currentUserId,
                    Content = commentModel.Comment,
                    HeroId = commentModel.HeroId
                });
                this.Data.SaveChanges();

                var viewModel = new HeroComment
                {
                    Author = currentUser,
                    Content = commentModel.Comment
                };
                return PartialView("_CommentPartial", viewModel);
            }
            else
            {
                // HttpResponceMessage needs using: using System.Net.Http;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
            }
        }

        public ActionResult Vote(int id)
        {
            var userId = this.User.Identity.GetUserId();
            var canVote = !this.Data.HeroVotes.All().Any(x => x.HeroId == id && x.VotedById == userId);
            if (canVote)
            {
                this.Data.Heroes.Find(id).Votes.Add(new HeroVote
                {
                    HeroId = id,
                    VotedById = userId
                });

                this.Data.SaveChanges();
            }

            var votes = this.Data.Heroes.Find(id).Votes.Count();

            return Content(votes.ToString());
        }
    }
}