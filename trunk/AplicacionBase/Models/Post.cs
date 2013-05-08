using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
            this.aspnet_Roles = new List<aspnet_Roles>();
        }

        public System.Guid Id { get; set; }
        public System.Guid IdUser { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Content { get; set; }
        public System.DateTime PublicationDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<aspnet_Roles> aspnet_Roles { get; set; }
    }
}
