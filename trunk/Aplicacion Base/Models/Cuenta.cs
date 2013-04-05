using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Cuenta
    {
        public System.Guid Id { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string Email { get; set; }
        public string Tipocuenta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
