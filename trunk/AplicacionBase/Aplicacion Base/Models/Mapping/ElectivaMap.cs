using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ElectivaMap : EntityTypeConfiguration<Electiva>
    {
        public ElectivaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NombreElectiva)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Electivas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NombreElectiva).HasColumnName("NombreElectiva");

            // Relationships
            this.HasMany(t => t.Estudios)
                .WithMany(t => t.Electivas)
                .Map(m =>
                    {
                        m.ToTable("ElectivasEstudios");
                        m.MapLeftKey("IdElectiva");
                        m.MapRightKey("IdEstudios");
                    });


        }
    }
}
