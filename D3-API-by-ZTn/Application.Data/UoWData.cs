using Application.Data.Repositories;
using Application.Models;
using Application.Models.Careers;
using Application.Models.Guides;
using Application.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class UoWData : IUoWData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public UoWData()
            : this(new ApplicationDbContext())
        {
        }

        public UoWData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Career> Careers
        {
            get { return this.GetRepository<Career>(); }
        }
        public IRepository<Hero> Heroes
        {
            get { return this.GetRepository<Hero>(); }
        }

        public IRepository<HeroComment> HeroComments
        {
            get { return this.GetRepository<HeroComment>(); }
        }

        public IRepository<HeroVote> HeroVotes
        {
            get { return this.GetRepository<HeroVote>(); }
        }

        public IRepository<Models.Guides.Guide> Guides
        {
            get { return this.GetRepository<Guide>(); }
        }

        public IRepository<Models.Guides.GuideComment> GuideComments
        {
            get { return this.GetRepository<GuideComment>(); }
        }

        public IRepository<Models.Guides.GuideVote> GuideVotes
        {
            get { return this.GetRepository<GuideVote>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

    }
}
