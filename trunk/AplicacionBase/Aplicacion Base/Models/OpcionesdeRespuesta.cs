using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class OpcionesdeRespuesta
    {
        public OpcionesdeRespuesta()
        {
            this.Respuestas = new List<Respuesta>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdPregunta { get; set; }
        public string Enunciado { get; set; }
        public decimal ValorN { get; set; }
        public string Tipo { get; set; }
        public decimal Numero { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
