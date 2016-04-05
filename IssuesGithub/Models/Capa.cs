using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssuesGithub.Models
{
    public class Capa
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        [DisplayName("Propietario")]
        public int PropietarioId { get; set; }

        [DisplayName("Responsable")]
        public int ResponableId { get; set; }



        public UserViewModel Propietario { get; set; }

        public UserViewModel Responsable { get; set; }
    }
}