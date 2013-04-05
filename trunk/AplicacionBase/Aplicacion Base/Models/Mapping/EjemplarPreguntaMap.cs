using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class EjemplarPreguntaMap : EntityTypeConfiguration<EjemplarPregunta>
    {
        public EjemplarPreguntaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdPregunta, t.IdEjemplar });

            // Properties
            // Table & Column Mappings
            this.ToTable("EjemplarPreguntas");
            this.Property(t => t.IdPregunta).HasColumnName("IdPregunta");
            this.Property(t => t.IdEjemplar).HasColumnName("IdEjemplar");

            // Relationships
            this.HasRequired(t => t.Ejemplare)
                .WithMany(t => t.EjemplarPreguntas)
                .HasForeignKey(d => d.IdEjemplar);
            this.HasRequired(t => t.Pregunta)
                .WithMany(t => t.EjemplarPreguntas)
                .HasForeignKey(d => d.IdPregunta);

        }
    }
}
