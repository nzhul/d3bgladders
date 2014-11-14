using Application.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Heroes
{
    public class HeroItems
    {
        public int Id { get; set; }
        public virtual Item Head { get; set; }

        public virtual Item Torso { get; set; }

        public virtual Item Feet { get; set; }

        public virtual Item Hands { get; set; }

        public virtual Item Shoulders { get; set; }

        public virtual Item Legs { get; set; }

        public virtual Item Bracers { get; set; }

        public virtual Item MainHand { get; set; }

        public virtual Item OffHand { get; set; }

        public virtual Item Waist { get; set; }

        public virtual Item RightFinger { get; set; }

        public virtual Item LeftFinger { get; set; }

        public virtual Item Neck { get; set; }
    }
}
