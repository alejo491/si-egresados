using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AplicacionBase.Models
{
    public partial class Vacancy
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el cargo ofrecido en la vacante")]
        [Display(Name = "Cargo")]
        public string Charge { get; set; }

        [Required(ErrorMessage = "Por favor ingrese la jornada de la vacante")]
        [Display(Name = "Jornada")]
        public string DayTrip { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el número de horas de trabajo de la vacante")]
        [Display(Name = "Número de Horas")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        public Nullable<decimal> HoursNumber { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el perfil profesional requerido")]
        [Display(Name = "Perfil Profesional")]
        public string ProfessionalProfile { get; set; }


        [Display(Name = "Fecha de Publicación")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:f}")]
        public System.DateTime PublicationDate { get; set; }



        [Required(ErrorMessage = "Por favor ingrese la fecha en la cierra la vacante")]
        [Display(Name = "Fecha de Cierre")]
        [Date(ErrorMessage="La fecha de cierre de la vacante debe ser posterior a la fecha actual")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:D}")]
        public System.DateTime ClosingDate { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el numero de vacantes ofrecidas")]
        [Display(Name = "Número de Vacantes ofrecidas")]
        [RegularExpression(@"^[0-9]*", ErrorMessage = " Este valor debe ser numérico")]
        public string VacanciesNumber { get; set; }

        [Required(ErrorMessage = "Por favor ingrese el salario ofrecido en la vacante")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]      
        [RegularExpression(@"^[0-9]*([\,][0-9]{1,2})?$", ErrorMessage = "Formato inadecuado para salario.")]
        [Display(Name = "Salario")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una descripción de la vacante")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }


        public System.Guid IdUser { get; set; }

        [Required(ErrorMessage = "Por favor especifique la compañía que ofrece la(s) vacante(s). ")]
        [Display(Name = "Compañía oferente de la vacante")]
        public System.Guid IdCompanie { get; set; }

        [Display(Name = "Compañía")]
        public virtual Company Company { get; set; }


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
