using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AplicacionBase.Models.Mapping
{
    public class StartboxMap : EntityTypeConfiguration<Startbox>
    {
        public StartboxMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Startboxs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Id_Post).HasColumnName("Id_Post");
            this.Property(t => t.Id_User).HasColumnName("Id_User");
            this.Property(t => t.Qualification).HasColumnName("Qualification");
        }
    }
}
