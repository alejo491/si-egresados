using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Encuesta
    {
        public Encuesta()
        {
            this.Ejemplares = new List<Ejemplare>();
            this.Temas = new List<Tema>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Objetivo { get; set; }
        public virtual ICollection<Ejemplare> Ejemplares { get; set; }
        public virtual ICollection<Tema> Temas { get; set; }
    }
}
