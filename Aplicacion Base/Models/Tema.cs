using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Tema
    {
        public Tema()
        {
            this.Preguntas = new List<Pregunta>();
            this.Encuestas = new List<Encuesta>();
        }

        public System.Guid Id { get; set; }
        public string Descipcion { get; set; }
        public virtual ICollection<Pregunta> Preguntas { get; set; }
        public virtual ICollection<Encuesta> Encuestas { get; set; }
    }
}
