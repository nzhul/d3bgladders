using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models.Guides
{
    public class GuideComment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int GuideId { get; set; }

        public virtual Guide Guide { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
