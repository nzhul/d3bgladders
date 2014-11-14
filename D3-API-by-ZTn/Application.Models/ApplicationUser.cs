using Application.Models.Guides;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Guide> guides;
        public ApplicationUser()
            :base()
        {
            this.guides = new HashSet<Guide>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //[MaxLength(50)]
        //[Index(IsUnique = true)]
        public string BattleTag { get; set; }

        public int PostsCount { get; set; }

        public virtual ICollection<Guide> Guides
        {
            get { return this.guides; }
            set { this.guides = value; }
        }

    }
}
