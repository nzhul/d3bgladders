using Application.Models.Careers;
using Application.Models.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Heroes
{
    public class Hero
    {
        private ICollection<Skill> activeSkills;
        private ICollection<Skill> passiveSkills;
        private ICollection<HeroComment> comments;
        private ICollection<HeroVote> votes;
        public Hero()
        {
            this.activeSkills = new HashSet<Skill>();
            this.passiveSkills = new HashSet<Skill>();
            this.comments = new HashSet<HeroComment>();
            this.votes = new HashSet<HeroVote>();
        }

        public int ID { get; set; }

        public int CareerId { get; set; }
        public virtual Career Career { get; set; }

        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        public virtual HeroItems Items { get; set; }

        public virtual HeroStats Stats { get; set; }

        public String Name { get; set; }

        public String InGameId { get; set; }

        public int Level { get; set; }

        public Boolean Seasonal { get; set; }

        public Boolean Hardcore { get; set; }

        public int ParagonLevel { get; set; }

        public virtual HeroGender Gender { get; set; }

        public Boolean Dead { get; set; }

        public virtual HeroClass HeroClass { get; set; }

        public virtual ICollection<Skill> ActiveSkills
        {
            get { return this.activeSkills; }
            set { this.activeSkills = value; }
        }

        public virtual ICollection<Skill> PassiveSkills
        {
            get { return this.passiveSkills; }
            set { this.passiveSkills = value; }
        }

        public virtual ICollection<HeroComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<HeroVote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }
    }
}
