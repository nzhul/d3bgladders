using Application.Data.Migrations;
using Application.Models;
using Application.Models.Careers;
using Application.Models.Heroes;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("d3bgladdersConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());z
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Career> Careers { get; set; }
        public IDbSet<Hero> Heroes { get; set; }

    }
}
