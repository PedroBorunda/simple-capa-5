using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssuesGithub.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaLimite { get; set; }

        [DisplayName("Estatus")]
        public int EstatusId { get; set; }

        [DisplayName("Tarea")]
        public int TipoTareaId { get; set; }

        [DisplayName("Archivo")]
        public int ArchivoId { get; set; }

        [DisplayName("Capa")]
        public int CapaId { get; set; }

        [DisplayName("Responsable")]
        public int ResponsableId { get; set; }




        public Estatus TipoEstatus { get; set; }

        public TipoTarea TipoTarea { get; set; }

        public Capa Capa { get; set; }

        public UserViewModel Responsable { get; set; }
    }
}