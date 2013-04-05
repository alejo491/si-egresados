using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Ciudad)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Sector)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Empresas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Ciudad).HasColumnName("Ciudad");
            this.Property(t => t.Sector).HasColumnName("Sector");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
        }
    }
}
