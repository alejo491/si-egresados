using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Jefe
    {
        public Jefe()
        {
            this.ExpJefes = new List<ExpJefe>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public virtual ICollection<ExpJefe> ExpJefes { get; set; }
    }
}
