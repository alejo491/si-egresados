using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class VacanteMap : EntityTypeConfiguration<Vacante>
    {
        public VacanteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Cargo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Jornada)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PerfilProfesional)
                .IsRequired();

            this.Property(t => t.NumeroVacantes)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Vacantes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Cargo).HasColumnName("Cargo");
            this.Property(t => t.Jornada).HasColumnName("Jornada");
            this.Property(t => t.NumHoras).HasColumnName("NumHoras");
            this.Property(t => t.PerfilProfesional).HasColumnName("PerfilProfesional");
            this.Property(t => t.FechaPublicacion).HasColumnName("FechaPublicacion");
            this.Property(t => t.FechaCierre).HasColumnName("FechaCierre");
            this.Property(t => t.NumeroVacantes).HasColumnName("NumeroVacantes");
            this.Property(t => t.Sueldo).HasColumnName("Sueldo");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");

            // Relationships
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.Vacantes)
                .HasForeignKey(d => d.IdEmpresa);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Vacantes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
