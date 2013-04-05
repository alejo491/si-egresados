using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public System.Guid IdReporte { get; set; }
        public string NombreTabla { get; set; }
        public string NombreCampo { get; set; }
        public decimal NumeroPagina { get; set; }
        public virtual Reporte Reporte { get; set; }
    }
}
