using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa los usurios registrados en el sistema
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public User()
        {
            this.Accounts = new List<Account>();
            this.Comments = new List<Comment>();
            this.Experiences = new List<Experience>();
            this.Posts = new List<Post>();
            this.Reports = new List<Report>();
            this.Studies = new List<Study>();
            this.UsersSteps = new List<UsersStep>();
            this.Vacancies = new List<Vacancy>();
            this.Likes = new List<Like>();
            this.Startboxs = new List<Startbox>();
        }

        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public System.Guid Id { get; set; }

        /// <summary>
        /// Tel�fono del usuario
        /// </summary>
        [DisplayName("Tel�fono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Tel�fono")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Nombres del usuario
        /// </summary>
        [Required]
        [DisplayName("Nombres *")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string FirstNames { get; set; }

        /// <summary>
        /// Apellidos del usuario
        /// </summary>
        [Required]
        [DisplayName("Apellidos *")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string LastNames { get; set; }

        /// <summary>
        /// Direcci�n del usuario
        /// </summary>
        [DisplayName("Direcci�n")]
        public string Address { get; set; }

        /// <summary>
        /// Celular del usuario
        /// </summary>
        [DisplayName("Celular")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "El formato es incorrecto")]
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Fecha de nacimiento del usuario
        /// </summary>
        [DisplayName("Fecha de Nacimiento")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        /// <summary>
        /// G�nero del usuario
        /// </summary>
        [DisplayName("G�nero")]
        public string Gender { get; set; }

        /// <summary>
        /// Estado civ�l del usuario
        /// </summary>
        [DisplayName("Estado Civil")]
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Estado del usuario
        /// </summary>
        [DisplayName("Estado")]
        public string States { get; set; }

        /// <summary>
        /// Colecci�n de cuentas del usuario
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; }
        
        /// <summary>
        /// Usuarios
        /// </summary>
        public virtual aspnet_Users aspnet_Users { get; set; }

        /// <summary>
        /// Colecci�n de comentarios del usuario
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Colecci�n de Experiencias del usuario
        /// </summary>
        public virtual ICollection<Experience> Experiences { get; set; }
        /// <summary>
        /// Colecci�n de noticias del usuario
        /// </summary>
        public virtual ICollection<Post> Posts { get; set; }
        /// <summary>
        /// Colecci�n de reportes del usuario
        /// </summary>
        public virtual ICollection<Report> Reports { get; set; }
        /// <summary>
        /// Colecci�n de estudios del usuario
        /// </summary>
        public virtual ICollection<Study> Studies { get; set; }
        /// <summary>
        /// Colecci�n de pasos de activaci�n para el wizard del usuario
        /// </summary>
        public virtual ICollection<UsersStep> UsersSteps { get; set; }
        /// <summary>
        /// Colecci�n de vacantes del usuario
        /// </summary>
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        /// <summary>
        /// Colecci�n de intereses del usuario
        /// </summary>
        public virtual ICollection<Like> Likes { get; set; }
        /// <summary>
        /// Colecci�n de startbox del usuario
        /// </summary>
        public virtual ICollection<Startbox> Startboxs { get; set; }
    }
}
