using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class Startbox
    {
        public System.Guid Id { get; set; }
        public System.Guid Id_Post { get; set; }
        public System.Guid Id_User { get; set; }
        public double Qualification { get; set; }
    }
}
