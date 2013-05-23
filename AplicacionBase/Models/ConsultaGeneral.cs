using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class ConsultaGeneral
    {
        public string Telefono { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Titulo { get; set; }
        public System.DateTime FechaInicioEstudio { get; set; }
        public System.DateTime FechaFinEstudio { get; set; }
        public string NombreElectiva { get; set; }
        public string TituloTesis { get; set; }
        public string DescripcionTesis { get; set; }
        public string NombreInstitucion { get; set; }
        public string Cargo { get; set; }
        public System.DateTime FechaInicioExperiencia { get; set; }
        public System.DateTime FechaFinExperiencia { get; set; }
        public string DescripcionExperiencia { get; set; }
        public System.DateTime FechaInicioExperienciaJefe { get; set; }
        public System.DateTime FechaFinExperienciaJefe { get; set; }
        public string NombreJeve { get; set; }
        public string EmailJefe { get; set; }
        public string TelefonoJefe { get; set; }
        public string NombreCompania { get; set; }
        public string Ciudad { get; set; }
        public string Sector { get; set; }
        public string Tipo { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Rol { get; set; }
        public string DescripcionRol { get; set; }
        public string EmailUsuario { get; set; }
    }
}
