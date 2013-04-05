using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Ejemplare
    {
        public Ejemplare()
        {
            this.EjemplarPreguntas = new List<EjemplarPregunta>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdEncuesta { get; set; }
        public decimal Numero { get; set; }
        public System.Guid IdEncuestado { get; set; }
        public virtual Encuesta Encuesta { get; set; }
        public virtual Encuestado Encuestado { get; set; }
        public virtual ICollection<EjemplarPregunta> EjemplarPreguntas { get; set; }
    }
}
