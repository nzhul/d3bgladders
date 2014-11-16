﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Application.Web.Models;
using Application.Models;
using Application.Data;
using ZTn.BNet.D3;
using ZTn.BNet.BattleNet;
using Application.Models.Careers;

namespace Application.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (db.Users.Any(x => x.BattleTag == model.BattleTag))
            {
                ModelState.AddModelError("BattleTag", "This Battle Tag is allready registered!");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, BattleTag = model.BattleTag };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                    // Add battletag data

                    var createdUser = db.Users.FirstOrDefault(x => x.Email == model.Email);

                    D3Api.ApiKey = "zrxxcy3qzp8jcbgrce2es4yq52ew2k7r"; //zrxxcy3qzp8jcbgrce2es4yq52ew2k7r // h74mhzdxmzqx3ugbggq955wan3kxfcga
                    var battleTag = new BattleTag(model.BattleTag);

                    var careerJS = ZTn.BNet.D3.Careers.Career.CreateFromBattleTag(battleTag);
                    var careerDB = new Application.Models.Careers.Career
                    {
                        BattleTag = battleTag.Id,
                        ApplicationUser = createdUser,
                        Kills = new Application.Models.Careers.CareerKills
                        {
                            elites = careerJS.Kills.elites,
                            hardcoreMonsters = careerJS.Kills.hardcoreMonsters,
                            monsters = careerJS.Kills.monsters
                        },
                        ParagonLevel = careerJS.ParagonLevel,
                        ParagonLevelHardcore = careerJS.ParagonLevelHardcore,
                        ParagonLevelSeason = careerJS.ParagonLevelSeason,
                        ParagonLevelSeasonHardcore = careerJS.ParagonLevelSeasonHardcore,
                        Progression = new Application.Models.Careers.CareerProgress
                        {
                            Act1 = careerJS.Progression.Act1,
                            Act2 = careerJS.Progression.Act2,
                            Act3 = careerJS.Progression.Act3,
                            Act4 = careerJS.Progression.Act4,
                            Act5 = careerJS.Progression.Act5
                        },
                        TimePlayed = new Application.Models.Careers.ClassTimePlayed
                        {
                            Barbarian = careerJS.TimePlayed.Barbarian,
                            Crusader = careerJS.TimePlayed.Crusader,
                            DemonHunter = careerJS.TimePlayed.DemonHunter,
                            Monk = careerJS.TimePlayed.Monk,
                            WitchDoctor = careerJS.TimePlayed.WitchDoctor,
                            Wizard = careerJS.TimePlayed.Wizard
                        }
                    };

                    db.Careers.Add(careerDB);
                    db.SaveChanges();

                    // Populating Heroes
                    for (int i = 0; i < 4; i++) //careerJS.Heroes.Count()
                    {
                        var heroJS = ZTn.BNet.D3.Heroes.Hero.CreateFromHeroId(battleTag, careerJS.Heroes[i].Id);
                        var heroDBClass = Application.Models.Heroes.HeroClass.Unknown;
                        switch (heroJS.HeroClass)
                        {
                            case ZTn.BNet.D3.Heroes.HeroClass.Unknown:
                                heroDBClass = Application.Models.Heroes.HeroClass.Unknown;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.Barbarian:
                                heroDBClass = Application.Models.Heroes.HeroClass.Barbarian;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.Crusader:
                                heroDBClass = Application.Models.Heroes.HeroClass.Crusader;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.DemonHunter:
                                heroDBClass = Application.Models.Heroes.HeroClass.DemonHunter;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.Monk:
                                heroDBClass = Application.Models.Heroes.HeroClass.Monk;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.WitchDoctor:
                                heroDBClass = Application.Models.Heroes.HeroClass.WitchDoctor;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.Wizard:
                                heroDBClass = Application.Models.Heroes.HeroClass.Wizard;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.TemplarFollower:
                                heroDBClass = Application.Models.Heroes.HeroClass.Unknown;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.ScoundrelFollower:
                                heroDBClass = Application.Models.Heroes.HeroClass.Unknown;
                                break;
                            case ZTn.BNet.D3.Heroes.HeroClass.EnchantressFollower:
                                heroDBClass = Application.Models.Heroes.HeroClass.Unknown;
                                break;
                            default:
                                break;
                        }
                        var heroDB = new Application.Models.Heroes.Hero
                        {
                            ApplicationUser = createdUser,
                            CareerId = careerDB.ID, // Check this if error!
                            Dead = heroJS.Dead,
                            Gender = heroJS.Gender == ZTn.BNet.D3.Heroes.HeroGender.Female ? Application.Models.Heroes.HeroGender.Female : Application.Models.Heroes.HeroGender.Male,
                            Hardcore = heroJS.Hardcore,
                            HeroClass = heroDBClass,
                            InGameId = heroJS.Id,
                            Level = heroJS.Level,
                            Name = heroJS.Name,
                            ParagonLevel = heroJS.ParagonLevel,
                            Seasonal = heroJS.Seasonal,
                            Stats = new Application.Models.Heroes.HeroStats
                            {
                                ArcaneResist = heroJS.Stats.ArcaneResist,
                                Armor = heroJS.Stats.Armor,
                                AttackSpeed = heroJS.Stats.AttackSpeed,
                                BlockAmountMax = heroJS.Stats.BlockAmountMax,
                                BlockAmountMin = heroJS.Stats.BlockAmountMin,
                                BlockChance = heroJS.Stats.BlockChance,
                                ColdResist = heroJS.Stats.ColdResist,
                                CritChance = heroJS.Stats.CritChance,
                                CritDamage = heroJS.Stats.CritDamage,
                                Damage = heroJS.Stats.Damage,
                                DamageIncrease = heroJS.Stats.DamageIncrease,
                                DamageReduction = heroJS.Stats.DamageReduction,
                                Dexterity = heroJS.Stats.Dexterity,
                                FireResist = heroJS.Stats.FireResist,
                                GoldFind = heroJS.Stats.GoldFind,
                                Healing = heroJS.Stats.Healing,
                                Intelligence = heroJS.Stats.Intelligence,
                                Life = heroJS.Stats.Life,
                                LifeOnHit = heroJS.Stats.LifeOnHit,
                                LifePerKill = heroJS.Stats.LifePerKill,
                                LifeSteal = heroJS.Stats.LifeSteal,
                                LightningResist = heroJS.Stats.LightningResist,
                                MagicFind = heroJS.Stats.MagicFind,
                                PhysicalResist = heroJS.Stats.PhysicalResist,
                                PoisonResist = heroJS.Stats.PoisonResist,
                                PrimaryResource = heroJS.Stats.PrimaryResource,
                                SecondaryResource = heroJS.Stats.SecondaryResource,
                                Strength = heroJS.Stats.Strength,
                                Thorns = heroJS.Stats.Thorns,
                                Toughness = heroJS.Stats.Toughness,
                                Vitality = heroJS.Stats.Vitality
                            }
                        };

                        // Populating Active Skill
                        for (int j = 0; j < heroJS.Skills.Active.Length; j++)
                        {
                            var jsSkill = heroJS.Skills.Active[j].Skill;
                            if (jsSkill != null)
                            {
                                var currentActiveSkill = new Application.Models.Skills.Skill
                                {
                                    Description = jsSkill.Description,
                                    Flavor = jsSkill.Flavor,
                                    Icon = jsSkill.Icon,
                                    IsPassive = false,
                                    Level = jsSkill.Level,
                                    Name = jsSkill.Name,
                                    Slug = jsSkill.Slug,
                                    TooltipUrl = jsSkill.TooltipUrl
                                };
                                heroDB.ActiveSkills.Add(currentActiveSkill);
                            }
                        }

                        // Populating Passive Skills
                        for (int k = 0; k < heroJS.Skills.Passive.Length; k++)
                        {
                            var jsSkillPassive = heroJS.Skills.Passive[k].Skill;
                            if (jsSkillPassive != null)
                            {
                                var currentPassiveSkill = new Application.Models.Skills.Skill
                                {
                                    Description = jsSkillPassive.Description,
                                    Flavor = jsSkillPassive.Flavor,
                                    Icon = jsSkillPassive.Icon,
                                    IsPassive = true,
                                    Level = jsSkillPassive.Level,
                                    Name = jsSkillPassive.Name,
                                    Slug = jsSkillPassive.Slug,
                                    TooltipUrl = jsSkillPassive.TooltipUrl
                                };
                                heroDB.PassiveSkills.Add(currentPassiveSkill);
                            }
                        }

                        db.Heroes.Add(heroDB);
                    }
                    db.SaveChanges();

                    //for (int i = 0; i < careerJS.Heroes.Count(); i++)
                    //{
                    //    var currentHero = ZTn.BNet.D3.Heroes.Hero.CreateFromHeroId(battleTag, careerJS.Heroes[i].Id);

                    //}

                    // Add battletag data END!!!

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}