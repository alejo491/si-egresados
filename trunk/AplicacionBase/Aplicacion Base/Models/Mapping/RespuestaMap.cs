using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class RespuestaMap : EntityTypeConfiguration<Respuesta>
    {
        public RespuestaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Respuestas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdRespuesta).HasColumnName("IdRespuesta");
            this.Property(t => t.IdPregunta).HasColumnName("IdPregunta");
            this.Property(t => t.IdEjemplar).HasColumnName("IdEjemplar");
            this.Property(t => t.ValorTexto).HasColumnName("ValorTexto");

            // Relationships
            this.HasRequired(t => t.EjemplarPregunta)
                .WithMany(t => t.Respuestas)
                .HasForeignKey(d => new { d.IdPregunta, d.IdEjemplar });
            this.HasRequired(t => t.OpcionesdeRespuesta)
                .WithMany(t => t.Respuestas)
                .HasForeignKey(d => d.IdRespuesta);

        }
    }
}
