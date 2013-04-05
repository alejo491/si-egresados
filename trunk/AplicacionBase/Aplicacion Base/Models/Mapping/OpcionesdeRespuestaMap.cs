using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class OpcionesdeRespuestaMap : EntityTypeConfiguration<OpcionesdeRespuesta>
    {
        public OpcionesdeRespuestaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Enunciado)
                .IsRequired();

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("OpcionesdeRespuestas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdPregunta).HasColumnName("IdPregunta");
            this.Property(t => t.Enunciado).HasColumnName("Enunciado");
            this.Property(t => t.ValorN).HasColumnName("ValorN");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Numero).HasColumnName("Numero");

            // Relationships
            this.HasRequired(t => t.Pregunta)
                .WithMany(t => t.OpcionesdeRespuestas)
                .HasForeignKey(d => d.IdPregunta);

        }
    }
}
