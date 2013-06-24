using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Representa la vista de la base de datos
    /// </summary>
    public partial class Post
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Post()
        {
            this.Comments = new List<Comment>();
            this.aspnet_Roles = new List<aspnet_Roles>();
            this.Likes = new List<Like>();
            this.Startboxs = new List<Startbox>();
            this.FilesPosts = new List<FilesPost>();
        }
        /// <summary>
        /// Identificador del post
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public System.Guid IdUser { get; set; }
        /// <summary>
        /// T�tulo del post
        /// </summary>
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("T�tulo")]
        [RegularExpression(@"[(A-Za-z������������\s,;:.""''��0-9@�#$%/=�?!�~*|&)-_]*", ErrorMessage = "El formato es incorrecto")]
        public string Title { get; set; }
        /// <summary>
        /// Resumen del post
        /// </summary>
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Resumen")]
        [RegularExpression(@"[(A-Za-z������������\s,;:.""''��0-9@�#$%/=�?!�~*|&)-_]*", ErrorMessage = "El formato es incorrecto")]
        public string Abstract { get; set; }
        /// <summary>
        /// Contenido del post
        /// </summary>
        [Required(ErrorMessage = "El campo es obligatorio")]
        [DisplayName("Contenido")]
        [RegularExpression(@"[(A-Za-z������������\s,;:.""''��0-9@�#$%/=�?!�~*|&)-_]*", ErrorMessage = "El formato es incorrecto")]
        public string Content { get; set; }
        /// <summary>
        /// Fecha de publicaci�n del post
        /// </summary>
        [DisplayName("Fecha de publiacion")]
        public System.DateTime PublicationDate { get; set; }
        /// <summary>
        /// Fecha de actualizaci�n del post
        /// </summary>
        [DisplayName("Fecha de actualizaci�n")]
        public Nullable<System.DateTime> UpdateDate { get; set; }
        /// <summary>
        /// Campo autorizado del post
        /// </summary>
        public int Autorized { get; set; }
        /// <summary>
        /// Campo principal del post
        /// </summary>
        public int Main { get; set; }
        /// <summary>
        /// Campo de estado del post
        /// </summary>
        public int Estate { get; set; }
        /// <summary>
        /// Comentarios del post
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        /// <summary>
        /// Objeto de tipo User
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Rol para el post
        /// </summary>
        public virtual ICollection<aspnet_Roles> aspnet_Roles { get; set; }
        /// <summary>
        /// Likes del post
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }
        /// <summary>
        /// Calificaci�n del post
        /// </summary>
        public virtual ICollection<Startbox> Startboxs { get; set; }
        /// <summary>
        /// Archivos del post
        /// </summary>
        public virtual ICollection<FilesPost> FilesPosts { get; set; }
    }
}
