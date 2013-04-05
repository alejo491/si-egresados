using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ReporteMap : EntityTypeConfiguration<Reporte>
    {
        public ReporteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descipcion)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Reportes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Descipcion).HasColumnName("Descipcion");
            this.Property(t => t.Fecha).HasColumnName("Fecha");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Reportes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
