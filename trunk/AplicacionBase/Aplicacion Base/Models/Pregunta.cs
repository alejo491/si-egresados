using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            this.EjemplarPreguntas = new List<EjemplarPregunta>();
            this.OpcionesdeRespuestas = new List<OpcionesdeRespuesta>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdTema { get; set; }
        public string Tipo { get; set; }
        public string Enunciado { get; set; }
        public decimal Numero { get; set; }
        public decimal ExigeRespuesta { get; set; }
        public virtual ICollection<EjemplarPregunta> EjemplarPreguntas { get; set; }
        public virtual ICollection<OpcionesdeRespuesta> OpcionesdeRespuestas { get; set; }
        public virtual Tema Tema { get; set; }
    }
}
