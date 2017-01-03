using OMG.LunchPicker.Objects.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OMG.LunchPicker.Data.Mapping
{
    public class RatingMap : EntityTypeConfiguration<Rating>
    {
        public RatingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RestaurantId, t.UserId });

            // Properties
            this.Property(t => t.RestaurantId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Rating");
            this.Property(t => t.RestaurantId).HasColumnName("RestaurantId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.RatingValue).HasColumnName("RatingValue");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateModified).HasColumnName("DateModified");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");

            // Relationships
            this.HasRequired(t => t.Restaurant)
                .WithMany(t => t.Ratings)
                .HasForeignKey(d => d.RestaurantId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Ratings)
                .HasForeignKey(d => d.UserId);

        }
    }
}
