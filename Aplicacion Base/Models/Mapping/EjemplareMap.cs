using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class EjemplareMap : EntityTypeConfiguration<Ejemplare>
    {
        public EjemplareMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Ejemplares");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdEncuesta).HasColumnName("IdEncuesta");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.IdEncuestado).HasColumnName("IdEncuestado");

            // Relationships
            this.HasRequired(t => t.Encuesta)
                .WithMany(t => t.Ejemplares)
                .HasForeignKey(d => d.IdEncuesta);
            this.HasRequired(t => t.Encuestado)
                .WithMany(t => t.Ejemplares)
                .HasForeignKey(d => d.IdEncuestado);

        }
    }
}
