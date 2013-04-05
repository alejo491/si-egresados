using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Comment
    {
        public System.Guid Id { get; set; }
        public System.Guid IdPost { get; set; }
        public System.Guid IdUser { get; set; }
        public string Content { get; set; }
        public System.DateTime CommentDate { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
