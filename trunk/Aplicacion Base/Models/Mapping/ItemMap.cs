using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NombreTabla)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.NombreCampo)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Items");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdReporte).HasColumnName("IdReporte");
            this.Property(t => t.NombreTabla).HasColumnName("NombreTabla");
            this.Property(t => t.NombreCampo).HasColumnName("NombreCampo");
            this.Property(t => t.NumeroPagina).HasColumnName("NumeroPagina");

            // Relationships
            this.HasRequired(t => t.Reporte)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.IdReporte);

        }
    }
}
