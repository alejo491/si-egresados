using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Vacante
    {
        public System.Guid Id { get; set; }
        public string Cargo { get; set; }
        public string Jornada { get; set; }
        public Nullable<decimal> NumHoras { get; set; }
        public string PerfilProfesional { get; set; }
        public System.DateTime FechaPublicacion { get; set; }
        public System.DateTime FechaCierre { get; set; }
        public string NumeroVacantes { get; set; }
        public decimal Sueldo { get; set; }
        public System.Guid IdUsuario { get; set; }
        public System.Guid IdEmpresa { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
