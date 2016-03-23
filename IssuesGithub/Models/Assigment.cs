using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssuesGithub.Models
{
    public class Assigment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        public virtual UserViewModel Owner { get; set; }
    }
}