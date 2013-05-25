using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AplicacionBase.Models.Mapping
{
    public class LikeMap : EntityTypeConfiguration<Like>
    {
        public LikeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Likes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Id_Post).HasColumnName("Id_Post");
            this.Property(t => t.Id_User).HasColumnName("Id_User");
        }
    }
}
