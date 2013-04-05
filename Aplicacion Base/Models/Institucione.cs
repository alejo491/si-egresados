using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Institucione
    {
        public Institucione()
        {
            this.Estudios = new List<Estudio>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Estudio> Estudios { get; set; }
    }
}
