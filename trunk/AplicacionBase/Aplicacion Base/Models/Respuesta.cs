using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Respuesta
    {
        public System.Guid Id { get; set; }
        public System.Guid IdRespuesta { get; set; }
        public System.Guid IdPregunta { get; set; }
        public System.Guid IdEjemplar { get; set; }
        public string ValorTexto { get; set; }
        public virtual EjemplarPregunta EjemplarPregunta { get; set; }
        public virtual OpcionesdeRespuesta OpcionesdeRespuesta { get; set; }
    }
}
