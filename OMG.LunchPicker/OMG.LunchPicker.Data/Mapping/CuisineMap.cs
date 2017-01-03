using OMG.LunchPicker.Objects.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OMG.LunchPicker.Data.Mapping
{
    public class CuisineMap : EntityTypeConfiguration<Cuisine>
    {
        public CuisineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("Cuisine");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateModified).HasColumnName("DateModified");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");

            // Relationships
            this.HasMany(t => t.Restaurants)
                .WithMany(t => t.Cuisines)
                .Map(m =>
                {
                    m.ToTable("RestaurantCuisine");
                    m.MapLeftKey("Cuisine_Id");
                    m.MapRightKey("Restaurant_Id");
                });


        }
    }
}
