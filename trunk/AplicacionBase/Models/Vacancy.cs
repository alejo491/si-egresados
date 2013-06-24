using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AplicacionBase.Models
{
    /// <summary>
    /// Clase que representa una vacante laboral
    /// </summary>
    public partial class Vacancy
    {
        /// <summary>
        /// Identificador de la vacante
        /// </summary>
        public System.Guid Id { get; set; }


        /// <summary>
        /// Cargo que se ofrece en la vacante
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el cargo ofrecido en la vacante")]
        [Display(Name = "Cargo")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string Charge { get; set; }

        /// <summary>
        /// Jornada de trabajo para la vacante ofrecida
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la jornada de la vacante")]
        [Display(Name = "Jornada")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string DayTrip { get; set; }

        /// <summary>
        /// N�mero de horas de trabajo diarias del cargo ofrecido 
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el n�mero de horas de trabajo de la vacante")]
        [Display(Name = "N�mero de Horas diarias")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public Nullable<decimal> HoursNumber { get; set; }

        /// <summary>
        /// Perfil profesional requerido para aplicar a la vacante
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el perfil profesional requerido")]
        [Display(Name = "Perfil Profesional")]
        [RegularExpression(@"[A-Za-z������������\s]*", ErrorMessage = "El formato es incorrecto")]
        public string ProfessionalProfile { get; set; }

        /// <summary>
        /// Fecha de publicaci�n de la vacante
        /// </summary>
        [Display(Name = "Fecha de Publicaci�n")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:f}")]
        public System.DateTime PublicationDate { get; set; }

        /// <summary>
        /// Fecha de cierre o expiraci�n de la vacante
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese la fecha en la cierra la vacante")]
        [Display(Name = "Fecha de Cierre")]
        [Date(ErrorMessage="La fecha de cierre de la vacante debe ser posterior a la fecha actual")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime ClosingDate { get; set; }


        /// <summary>
        /// N�mero de vacantes ofrecidas por la compa��a
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el numero de vacantes ofrecidas")]
        [Display(Name = "N�mero de Vacantes ofrecidas")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = " Este valor debe ser num�rico")]
        public string VacanciesNumber { get; set; }


        /// <summary>
        /// Salario ofrecido
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese el salario ofrecido en la vacante")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]      
        [RegularExpression(@"^[0-9]*([\,][0-9]{1,2})?$", ErrorMessage = "Formato inadecuado para salario.")]
        [Display(Name = "Salario")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Descripci�n de las labores que deben desempe�arse
        /// </summary>
        [Required(ErrorMessage = "Por favor ingrese una descripci�n de la vacante")]
        [Display(Name = "Descripci�n")]
        [RegularExpression(@"[(A-Za-z������������\s,;:.""''��0-9@�#$%/=�?!�~*|&)-_]*", ErrorMessage = "El formato es incorrecto")]
        public string Description { get; set; }

        /// <summary>
        /// Identificador del usuario que publica la vacante
        /// </summary>
        public System.Guid IdUser { get; set; }

        /// <summary>
        /// Identificador de la compa��a que ofrece la vacante
        /// </summary>
        [Required(ErrorMessage = "Por favor especifique la compa��a que ofrece la(s) vacante(s). ")]
        [Display(Name = "Compa��a oferente de la vacante")]
        public System.Guid IdCompanie { get; set; }


        /// <summary>
        /// Objeto que referencia a la compa��a que ofrece la vacante
        /// </summary>
        [Display(Name = "Compa��a")]
        public virtual Company Company { get; set; }

        /// <summary>
        /// Objeto que referencia al usuario que publica la vacante
        /// </summary>
        public virtual User User { get; set; }
    }


     public sealed class DateAttribute : ValidationAttribute
      {
          public override bool IsValid(object value)
          {
              DateTime date = (DateTime)value;
              if (date.CompareTo(DateTime.Now) < 0)
              {
                  return false;
              }
              return true;
          }
      }
}
