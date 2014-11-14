using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models.Heroes
{
    public class HeroComment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int HeroId { get; set; }

        public virtual Hero Hero { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
