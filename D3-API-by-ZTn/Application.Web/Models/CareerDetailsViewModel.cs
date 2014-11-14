using Application.Models;
using Application.Models.Careers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class CareerDetailsViewModel
    {
        private IEnumerable<HeroViewModel> heroes;
        public CareerDetailsViewModel()
        {
            this.heroes = new HashSet<HeroViewModel>();
        }
        public int ID { get; set; }

        public string BattleTag { get; set; }

        public virtual CareerKills Kills { get; set; }

        public virtual ClassTimePlayed TimePlayed { get; set; }

        public int ParagonLevel { get; set; }

        public int ParagonLevelHardcore { get; set; }

        public int ParagonLevelSeason { get; set; }

        public int ParagonLevelSeasonHardcore { get; set; }

        public virtual CareerProgress Progression { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual IEnumerable<HeroViewModel> Heroes
        {
            get { return this.heroes; }
            set { this.heroes = value; }
        }
    }
}