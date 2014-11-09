using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Heroes
{
    public class HeroStats
    {
        public Double Toughness { get; set; }
        public Double Healing { get; set; }
        public Double Life { get; set; }
        public Double Damage { get; set; }
        public Double AttackSpeed { get; set; }
        public int Armor { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Vitality { get; set; }
        public int Intelligence { get; set; }
        public int PhysicalResist { get; set; }
        public int FireResist { get; set; }
        public int ColdResist { get; set; }
        public int LightningResist { get; set; }
        public int PoisonResist { get; set; }
        public int ArcaneResist { get; set; }
        public Double CritDamage { get; set; }
        public Double DamageIncrease { get; set; }
        public Double CritChance { get; set; }
        public Double DamageReduction { get; set; }
        public Double BlockChance { get; set; }
        public Double Thorns { get; set; }
        public Double LifeSteal { get; set; }
        public Double LifePerKill { get; set; }
        public Double GoldFind { get; set; }
        public Double MagicFind { get; set; }
        public Double BlockAmountMin { get; set; }
        public Double BlockAmountMax { get; set; }
        public Double LifeOnHit { get; set; }
        public int PrimaryResource { get; set; }
        public int SecondaryResource { get; set; }
    }
}
