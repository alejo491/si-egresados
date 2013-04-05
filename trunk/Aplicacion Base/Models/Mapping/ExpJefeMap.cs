using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ExpJefeMap : EntityTypeConfiguration<ExpJefe>
    {
        public ExpJefeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ExpJefes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdJefe).HasColumnName("IdJefe");
            this.Property(t => t.IdExpLaboral).HasColumnName("IdExpLaboral");
            this.Property(t => t.FechaInicio).HasColumnName("FechaInicio");
            this.Property(t => t.FechaFin).HasColumnName("FechaFin");

            // Relationships
            this.HasRequired(t => t.ExpLaborale)
                .WithMany(t => t.ExpJefes)
                .HasForeignKey(d => d.IdExpLaboral);
            this.HasRequired(t => t.Jefe)
                .WithMany(t => t.ExpJefes)
                .HasForeignKey(d => d.IdJefe);

        }
    }
}
