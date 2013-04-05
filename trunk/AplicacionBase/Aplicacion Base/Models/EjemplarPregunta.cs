using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class EjemplarPregunta
    {
        public EjemplarPregunta()
        {
            this.Respuestas = new List<Respuesta>();
        }

        public System.Guid IdPregunta { get; set; }
        public System.Guid IdEjemplar { get; set; }
        public virtual Ejemplare Ejemplare { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
