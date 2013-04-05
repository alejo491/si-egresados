using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AplicacionBase.Models.Mapping
{
    public class StepMap : EntityTypeConfiguration<Step>
    {
        public StepMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SPath)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Steps");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SOrder).HasColumnName("SOrder");
            this.Property(t => t.SPath).HasColumnName("SPath");
        }
    }
}
