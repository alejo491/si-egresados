using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Encuestado
    {
        public Encuestado()
        {
            this.Ejemplares = new List<Ejemplare>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public virtual ICollection<Ejemplare> Ejemplares { get; set; }
    }
}
