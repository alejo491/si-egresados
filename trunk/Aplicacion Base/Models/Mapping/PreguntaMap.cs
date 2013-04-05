using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class PreguntaMap : EntityTypeConfiguration<Pregunta>
    {
        public PreguntaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Enunciado)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Preguntas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdTema).HasColumnName("IdTema");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Enunciado).HasColumnName("Enunciado");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.ExigeRespuesta).HasColumnName("ExigeRespuesta");

            // Relationships
            this.HasRequired(t => t.Tema)
                .WithMany(t => t.Preguntas)
                .HasForeignKey(d => d.IdTema);

        }
    }
}
