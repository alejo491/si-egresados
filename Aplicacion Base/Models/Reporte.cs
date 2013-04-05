using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Reporte
    {
        public Reporte()
        {
            this.Items = new List<Item>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string Descipcion { get; set; }
        public System.DateTime Fecha { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
