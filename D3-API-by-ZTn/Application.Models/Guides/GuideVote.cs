using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models.Guides
{
    public class GuideVote
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string VotedById { get; set; }

        public virtual ApplicationUser VotedBy { get; set; }

        [Required]
        public int GuideId { get; set; }

        public virtual Guide Guide { get; set; }
    }
}
