using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssuesGithub.Models
{
    public class Estatus
    {
        [Key]
        public int Id { get; set; }

        public string Estado { get; set; }
    }
}