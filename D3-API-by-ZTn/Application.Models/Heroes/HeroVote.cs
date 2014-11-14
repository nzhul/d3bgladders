using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Heroes
{
    public class HeroVote
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string VotedById { get; set; }

        public virtual ApplicationUser VotedBy { get; set; }

        [Required]
        public int HeroId { get; set; }

        public virtual Hero Hero { get; set; }
    }
}
