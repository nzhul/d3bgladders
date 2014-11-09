using Application.Data.Repositories;
using Application.Models;
using Application.Models.Careers;
using Application.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public interface IUoWData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Career> Careers { get; }
        IRepository<Hero> Heroes { get; }

        int SaveChanges();
    }
}
