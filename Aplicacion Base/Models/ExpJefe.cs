using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class ExpJefe
    {
        public System.Guid Id { get; set; }
        public System.Guid IdJefe { get; set; }
        public System.Guid IdExpLaboral { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public virtual ExpLaborale ExpLaborale { get; set; }
        public virtual Jefe Jefe { get; set; }
    }
}
