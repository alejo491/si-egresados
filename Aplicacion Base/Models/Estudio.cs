using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Estudio
    {
        public Estudio()
        {
            this.Electivas = new List<Electiva>();
        }

        public System.Guid IdInstitucion { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string Titulo { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string TrabajoGrado { get; set; }
        public string DescripcionTrabajoGrado { get; set; }
        public System.Guid Id { get; set; }
        public virtual Institucione Institucione { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Electiva> Electivas { get; set; }
    }
}
