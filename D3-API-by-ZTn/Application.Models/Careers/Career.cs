﻿using Application.Models.Heroes;
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

        public virtual CareerKills Kills { get; set; }

        public virtual ClassTimePlayed TimePlayed { get; set; }

        public int ParagonLevel { get; set; }

        public int ParagonLevelHardcore { get; set; }

        public int ParagonLevelSeason { get; set; }

        public int ParagonLevelSeasonHardcore { get; set; }

        public virtual CareerProgress Progression { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Hero> Heroes
        {
            get { return this.heroes; }
            set { this.heroes = value; }
        }

    }
}
