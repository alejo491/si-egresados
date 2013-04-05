using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class TemaMap : EntityTypeConfiguration<Tema>
    {
        public TemaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descipcion)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Temas");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Descipcion).HasColumnName("Descipcion");
        }
    }
}
