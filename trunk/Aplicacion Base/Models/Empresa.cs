using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            this.ExpLaborales = new List<ExpLaborale>();
            this.Vacantes = new List<Vacante>();
        }

        public System.Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Sector { get; set; }
        public string Tipo { get; set; }
        public virtual ICollection<ExpLaborale> ExpLaborales { get; set; }
        public virtual ICollection<Vacante> Vacantes { get; set; }
    }
}
