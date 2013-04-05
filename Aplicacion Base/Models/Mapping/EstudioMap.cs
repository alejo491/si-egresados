using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class EstudioMap : EntityTypeConfiguration<Estudio>
    {
        public EstudioMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.TrabajoGrado)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Estudios");
            this.Property(t => t.IdInstitucion).HasColumnName("IdInstitucion");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.FechaInicio).HasColumnName("FechaInicio");
            this.Property(t => t.FechaFin).HasColumnName("FechaFin");
            this.Property(t => t.TrabajoGrado).HasColumnName("TrabajoGrado");
            this.Property(t => t.DescripcionTrabajoGrado).HasColumnName("DescripcionTrabajoGrado");
            this.Property(t => t.Id).HasColumnName("Id");

            // Relationships
            this.HasRequired(t => t.Institucione)
                .WithMany(t => t.Estudios)
                .HasForeignKey(d => d.IdInstitucion);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Estudios)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
