using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class JefeMap : EntityTypeConfiguration<Jefe>
    {
        public JefeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Telefono)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Jefes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Telefono).HasColumnName("Telefono");
        }
    }
}
