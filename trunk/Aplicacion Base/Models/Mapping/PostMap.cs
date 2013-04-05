using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Contenido)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Posts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Contenido).HasColumnName("Contenido");
            this.Property(t => t.FechaPublicacion).HasColumnName("FechaPublicacion");
            this.Property(t => t.Fecha).HasColumnName("Fecha");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Posts)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
