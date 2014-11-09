using Application.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class HeroViewModel
    {
        public int ID { get; set; }
        public string BattleTag { get; set; }
        public string Name { get; set; }
        public int ParagonLevel { get; set; }
        public HeroClass HeroClass { get; set; }
        public double Damage { get; set; }
    }
}