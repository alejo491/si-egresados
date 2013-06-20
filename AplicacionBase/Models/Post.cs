using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
            this.aspnet_Roles = new List<aspnet_Roles>();
            this.Likes = new List<Like>();
            this.Startboxs = new List<Startbox>();
            this.FilesPosts = new List<FilesPost>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUser { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Título")]
        [RegularExpression(@"[A-Za-zñÑáéíóúÁÉÍÓÚ\s,;:.""'']*", ErrorMessage = "El formato es incorrecto")]
        [MaxLength(100, ErrorMessage = "El título no pueder tener más de 100 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Resumen")]
        [RegularExpression(@"[A-Za-zñÑáéíóúÁÉÍÓÚ\s,;:.""'']*", ErrorMessage = "El formato es incorrecto")]
        public string Abstract { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Contenido")]
        [RegularExpression(@"[A-Za-zñÑáéíóúÁÉÍÓÚ\s,;:.""'']*", ErrorMessage = "El formato es incorrecto")]
        public string Content { get; set; }

        [DisplayName("Fecha de publiacion")]
        public System.DateTime PublicationDate { get; set; }

        [DisplayName("Fecha de actualización")]
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public int Autorized { get; set; }
        public int Main { get; set; }
        public int Estate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Startbox> Startboxs { get; set; }
        public virtual ICollection<FilesPost> FilesPosts { get; set; }
    }
}
