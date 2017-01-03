using OMG.LunchPicker.Objects;
using OMG.LunchPicker.Objects.Entities;
using OMG.LunchPicker.Temp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Temp.DataModel
{
    public class OmgContext : DbContext
    {

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaContext"/> class.
        /// </summary>
        public OmgContext() : base("name=OmgContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Public Properties
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<RestaurantCuisine> RestaurantCuisines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        #endregion

        #region Overrides
        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Types().Where(t => t is IModificationHistory).Configure(c => c.Ignore("IsDirty")); // THIS IS CLEANER
            //modelBuilder.Types().Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
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
            SetDates(); // set DateCreated, DateModified on targeted interface
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
            await SetDatesAsync();
            int result = await base.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
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
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            await SetDatesAsync();
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Sets the dates asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SetDatesAsync()
        {
            await Task.Run(() => SetDates());
            return true;
        }

        /// <summary>
        /// Sets the dates.
        /// </summary>
        private void SetDates()
        {
            foreach (var history in this.ChangeTracker.Entries())
            {
                if (history.Entity is IAuditable
                    && (history.State == EntityState.Added || history.State == EntityState.Modified))
                {
                    IAuditable modHist = history.Entity as IAuditable;
                    var timeStamp = DateTime.Now;

                    modHist.DateModified = timeStamp;
                    modHist.ModifiedBy = -1;

                    if (history.State == EntityState.Added)
                    {
                        modHist.DateCreated = timeStamp;
                        modHist.CreatedBy = -1;
                    }
                }
            }
        }
        #endregion
    }
}
