using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AplicacionBase.Models
{
    public partial class School
    {
        public School()
        {
            this.Studies = new List<Study>();
        }

        public System.Guid Id { get; set; }

        [DisplayName("Nombre de la Instituci�n")]
        public string Name { get; set; }
        public virtual ICollection<Study> Studies { get; set; }
    }
}
