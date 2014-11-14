using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class CareerSummaryViewModel
    {
        private IEnumerable<HeroViewModel> heroes;

        public CareerSummaryViewModel()
        {
            this.heroes = new List<HeroViewModel>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BattleTag { get; set; }
        [DisplayName("Posts Count")]
        public int PostCount { get; set; }
        public int MonsterKills { get; set; }
        public int EliteMonsterKills { get; set; }

        [DisplayName("HC Monster Kills")]
        public int HardcoreMonsterKills { get; set; }
        [DisplayName("Plvl")]
        public int ParagonLevel { get; set; }
        [DisplayName("Plvl-S")]
        public int ParagonLevelSeason { get; set; }
        [DisplayName("Plvl-S-HC")]
        public int ParagonLevelSeasonHardcore { get; set; }

        public virtual IEnumerable<HeroViewModel> Heroes
        {
            get { return this.heroes; }
            set { this.heroes = value; }
        }
    }
}