using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Electiva
    {
        public Electiva()
        {
            this.Estudios = new List<Estudio>();
        }

        public System.Guid Id { get; set; }
        public string NombreElectiva { get; set; }
        public virtual ICollection<Estudio> Estudios { get; set; }
    }
}
