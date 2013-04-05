using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class ExpLaborale
    {
        public ExpLaborale()
        {
            this.ExpJefes = new List<ExpJefe>();
        }

        public System.Guid IdUsuario { get; set; }
        public System.Guid Id { get; set; }
        public string Cargo { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string Labores { get; set; }
        public System.Guid IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<ExpJefe> ExpJefes { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
