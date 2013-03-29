using System;
using System.Collections.Generic;

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
        }

        public System.Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstNames { get; set; }
        public string LastNames { get; set; }
        public string Address { get; set; }
        public string CellphoneNumber { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
        public virtual ICollection<UsersStep> UsersSteps { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
