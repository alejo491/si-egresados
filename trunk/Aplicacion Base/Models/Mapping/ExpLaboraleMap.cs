using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ExpLaboraleMap : EntityTypeConfiguration<ExpLaborale>
    {
        public ExpLaboraleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Cargo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Labores)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ExpLaborales");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Cargo).HasColumnName("Cargo");
            this.Property(t => t.FechaInicio).HasColumnName("FechaInicio");
            this.Property(t => t.FechaFin).HasColumnName("FechaFin");
            this.Property(t => t.Labores).HasColumnName("Labores");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");

            // Relationships
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.ExpLaborales)
                .HasForeignKey(d => d.IdEmpresa);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.ExpLaborales)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
