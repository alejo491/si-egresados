using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class CuentaMap : EntityTypeConfiguration<Cuenta>
    {
        public CuentaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Tipocuenta)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Cuenta");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Tipocuenta).HasColumnName("Tipocuenta");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Cuentas)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
