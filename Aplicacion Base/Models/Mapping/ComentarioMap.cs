using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Aplicacion_Base.Models.Mapping
{
    public class ComentarioMap : EntityTypeConfiguration<Comentario>
    {
        public ComentarioMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Contenido)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Comentarios");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdPost).HasColumnName("IdPost");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Contenido).HasColumnName("Contenido");
            this.Property(t => t.Fecha).HasColumnName("Fecha");

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(d => d.IdPost);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
