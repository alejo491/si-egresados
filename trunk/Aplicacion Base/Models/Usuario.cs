using System;
using System.Collections.Generic;

namespace Aplicacion_Base.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            this.Comentarios = new List<Comentario>();
            this.Cuentas = new List<Cuenta>();
            this.Estudios = new List<Estudio>();
            this.ExpLaborales = new List<ExpLaborale>();
            this.Posts = new List<Post>();
            this.Reportes = new List<Reporte>();
            this.Vacantes = new List<Vacante>();
        }

        public System.Guid Id { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Cuenta> Cuentas { get; set; }
        public virtual ICollection<Estudio> Estudios { get; set; }
        public virtual ICollection<ExpLaborale> ExpLaborales { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
        public virtual ICollection<Vacante> Vacantes { get; set; }
    }
}
