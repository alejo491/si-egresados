using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AplicacionBase.Models.Mapping
{
    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Patch)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Size)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Files");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Patch).HasColumnName("Patch");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Size).HasColumnName("Size");
        }
    }
}
