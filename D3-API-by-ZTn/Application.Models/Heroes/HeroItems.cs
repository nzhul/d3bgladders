using Application.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Heroes
{
    public class HeroItems
    {
        public int ID { get; set; }
        public Item Head { get; set; }

        public Item Torso { get; set; }

        public Item Feet { get; set; }

        public Item Hands { get; set; }

        public Item Shoulders { get; set; }

        public Item Legs { get; set; }

        public Item Bracers { get; set; }

        public Item MainHand { get; set; }

        public Item OffHand { get; set; }

        public Item Waist { get; set; }

        public Item RightFinger { get; set; }

        public Item LeftFinger { get; set; }

        public Item Neck { get; set; }
    }
}
