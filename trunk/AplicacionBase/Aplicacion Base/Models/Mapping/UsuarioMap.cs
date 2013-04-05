using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Telefono)
                .HasMaxLength(20);

            this.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Apellidos)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Direccion)
                .HasMaxLength(50);

            this.Property(t => t.Celular)
                .HasMaxLength(20);

            this.Property(t => t.Genero)
                .HasMaxLength(1);

            this.Property(t => t.EstadoCivil)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Usuarios");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Telefono).HasColumnName("Telefono");
            this.Property(t => t.Nombre).HasColumnName("Nombre");
            this.Property(t => t.Apellidos).HasColumnName("Apellidos");
            this.Property(t => t.Direccion).HasColumnName("Direccion");
            this.Property(t => t.Celular).HasColumnName("Celular");
            this.Property(t => t.FechaNacimiento).HasColumnName("FechaNacimiento");
            this.Property(t => t.Genero).HasColumnName("Genero");
            this.Property(t => t.EstadoCivil).HasColumnName("EstadoCivil");

            // Relationships
            this.HasRequired(t => t.aspnet_Users)
                .WithOptional(t => t.Usuario);

        }
    }
}
