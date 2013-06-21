using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class User
    {
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

        public System.Guid Id { get; set; }

        [DisplayName("Telefono")]
        [RegularExpression(@"[0-9]{7,10}", ErrorMessage = " No tiene el formato de Telefono")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Nombres *")]
        [RegularExpression(@"[A-Za-zÒ—·ÈÌÛ˙¡…Õ”⁄\s]*", ErrorMessage = "El formato es incorrecto")]
        public string FirstNames { get; set; }

        [Required]
        [DisplayName("Apellidos *")]
        [RegularExpression(@"[A-Za-zÒ—·ÈÌÛ˙¡…Õ”⁄\s]*", ErrorMessage = "El formato es incorrecto")]
        public string LastNames { get; set; }

        [DisplayName("Direccion")]
        public string Address { get; set; }

        [DisplayName("Celular")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "El formato es incorrecto")]
        public string CellphoneNumber { get; set; }

        [DisplayName("Fecha de Nacimiento")]

        public Nullable<System.DateTime> BirthDate { get; set; }

        [DisplayName("Genero")]
        public string Gender { get; set; }

        [DisplayName("Estado Civil")]
        public string MaritalStatus { get; set; }

        [DisplayName("Estado")]
        public string States { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
        public virtual ICollection<UsersStep> UsersSteps { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Startbox> Startboxs { get; set; }
    }
}
