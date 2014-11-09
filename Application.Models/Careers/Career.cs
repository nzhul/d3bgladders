using Application.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Careers
{
    public class Career
    {
        private ICollection<Hero> heroes;
        public Career()
        {
            this.heroes = new HashSet<Hero>();
        }
        public int ID { get; set; }
        public string BattleTag { get; set; }

        public CareerKills Kills;

        public ClassTimePlayed TimePlayed;

        public int ParagonLevel;

        public int ParagonLevelHardcore;

        public int ParagonLevelSeason;

        public int ParagonLevelSeasonHardcore;

        public CareerProgress Progression;

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Hero> Heroes
        {
            get { return this.heroes; }
            set { this.heroes = value; }
        }

    }
}
