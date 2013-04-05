using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Comentario
    {
        public System.Guid Id { get; set; }
        public System.Guid IdPost { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string Contenido { get; set; }
        public System.DateTime Fecha { get; set; }
        public virtual Post Post { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
