using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Post
    {
        public Post()
        {
            this.Comentarios = new List<Comentario>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string Contenido { get; set; }
        public System.DateTime FechaPublicacion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
