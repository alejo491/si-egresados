using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AplicacionBase.Models.Mapping
{
    public class SurveyMap : EntityTypeConfiguration<Survey>
    {
        public SurveyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Aim)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Surveys");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Aim).HasColumnName("Aim");

            // Relationships
            this.HasMany(t => t.Topics)
                .WithMany(t => t.Surveys)
                .Map(m =>
                    {
                        m.ToTable("SurveysTopics");
                        m.MapLeftKey("IdSurveys");
                        m.MapRightKey("IdTopic");
                    });


        }
    }
}
