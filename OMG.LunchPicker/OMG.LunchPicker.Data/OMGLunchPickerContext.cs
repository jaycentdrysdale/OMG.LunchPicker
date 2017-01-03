using System.Data.Entity;
using OMG.LunchPicker.Data.Mapping;
using OMG.LunchPicker.Objects.Entities;
using System.Configuration;
using Repository.Pattern.Ef6;
using System.Threading.Tasks;
using OMG.LunchPicker.Objects;
using System;

namespace OMG.LunchPicker.Data
{
    public partial class OMGLunchPickerContext : DataContext
    {
        #region Ctors
        static OMGLunchPickerContext()
        {
            var rebuildDb = ConfigurationManager.AppSettings["RebuildDB"];
            Database.SetInitializer(rebuildDb == "yes" ? new DropCreateDatabaseAlways<OMGLunchPickerContext>() : null);
        }

        public OMGLunchPickerContext()
            : base("Name=OMGLunchPickerContext")
        {
        }
        #endregion

        #region DBSets
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region Overrides
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CuisineMap());
            modelBuilder.Configurations.Add(new RatingMap());
            modelBuilder.Configurations.Add(new RestaurantMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public override int SaveChanges()
        {
            HandleAuditing(); // set DateCreated, DateModified on targeted interface
            int result = base.SaveChanges();
            return result;
        }


        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        public override async Task<int> SaveChangesAsync()
        {
            await HandleAuditingAsync();
            int result = await base.SaveChangesAsync();
            return result;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Sets the dates asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> HandleAuditingAsync()
        {
            await Task.Run(() => HandleAuditing());
            return true;
        }

        private void HandleAuditing()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IAuditable>())
            {
                var currentUser = 1;
                //var windowsIdentity = isRunningUnitTests
                //    ? WindowsIdentity.GetCurrent()
                //    : HttpContext.Current.User.Identity;
                //if (windowsIdentity != null)
                //    currentUser = windowsIdentity.Name.Split('\\')[1];

                if (auditableEntity.State == EntityState.Added || auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.DateModified = DateTime.Now; //DateTime.UtcNow;
                    auditableEntity.Entity.ModifiedBy = currentUser;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.DateCreated = DateTime.Now; //DateTime.UtcNow;
                        auditableEntity.Entity.CreatedBy = currentUser;
                    }
                    else
                    {
                        auditableEntity.Property(p => p.DateCreated).IsModified = false;
                        auditableEntity.Property(p => p.CreatedBy).IsModified = false;
                    }
                }
            }
        }

        #endregion
    }
}
