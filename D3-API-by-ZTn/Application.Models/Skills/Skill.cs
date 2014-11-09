using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Skills
{
    public class Skill
    {
        public int ID { get; set; }

        public String Slug { get; set; }

        public String Name { get; set; }

        public String Icon { get; set; }

        public String TooltipUrl { get; set; }

        public String Description { get; set; }

        public String Flavor { get; set; }

        public bool IsPassive { get; set; }

        public int Level { get; set; }
    }
}
