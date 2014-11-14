using Application.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Guides
{
    public class Guide
    {
        private ICollection<GuideComment> comments;
        private ICollection<GuideVote> votes;
        public Guide()
        {
            this.comments = new HashSet<GuideComment>();
            this.votes = new HashSet<GuideVote>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int HeroId { get; set; }

        public virtual Hero Hero { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<GuideComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<GuideVote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }
    }
}
