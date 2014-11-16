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
    }
}