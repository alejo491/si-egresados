using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class EncuestaMap : EntityTypeConfiguration<Encuesta>
    {
        public EncuestaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Objetivo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Encuestas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Objetivo).HasColumnName("Objetivo");

            // Relationships
            this.HasMany(t => t.Temas)
                .WithMany(t => t.Encuestas)
                .Map(m =>
                    {
                        m.ToTable("EncuestaTemas");
                        m.MapLeftKey("IdEncuesta");
                        m.MapRightKey("IdTema");
                    });


        }
    }
}
