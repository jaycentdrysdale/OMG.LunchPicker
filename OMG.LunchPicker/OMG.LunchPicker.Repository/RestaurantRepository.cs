using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Repository
{
    public class RestaurantRepository : DefaultRepository, IRestaurantRepository
    {
        #region Ctors
        public RestaurantRepository(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync) { }
        #endregion

        #region IRestaurantRepository Members
        public async Task<IQueryable<dynamic>> GetAllAsync(GetRestaurantsCriteria criteria)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Restaurant>()
            .Queryable()
            .AsNoTracking()
            .Where(r => r.IsActive == true)
            .Select(r => new
            {
                r.Id,
                r.Name,
                r.Street,
                r.City,
                r.State,
                r.Zip,
                rating = (int)(r.Ratings.Any() ? r.Ratings.Average(x => x.RatingValue) : 0.0),
                cuisines = r.Cuisines.Where(rc => rc.IsActive).Select(rc => rc.Name)
            });

            if (!string.IsNullOrEmpty(criteria.PartialName))
                query = query.Where(q => q.Name.Contains(criteria.PartialName)).AsQueryable();

            if (criteria.RatingValue != null)
                query = query.Where(q => q.rating == criteria.RatingValue).AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Cuisine))
                query = query.Where(q => q.cuisines.Contains(criteria.Cuisine)).AsQueryable();

            return await Task.Run(() => query);
        }

        public async Task<dynamic> GetAsync(GetByIdCriteria criteria)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Restaurant>()
            .Queryable()
            .AsNoTracking()
            .Where(r => r.IsActive == criteria.IsActive && r.Id == criteria.Id)
            .Select(r => new
            {
                r.Id,
                r.Name,
                r.Street,
                r.City,
                r.State,
                r.Zip,
                rating = (int)(r.Ratings.Any() ? r.Ratings.Average(x => x.RatingValue) : 0.0),
                cuisines = r.Cuisines.Where(c => c.IsActive).Select(c => c.Name)
            })
            .SingleOrDefaultAsync();

            return await Task.Run(() => query);
        }

        public async Task<int> SaveAsync(SaveRestaurantCriteria criteria)
        {
            UnitOfWorkAsync.BeginTransaction();

            var attachedCuisines = new List<Cuisine>();
            Restaurant restaurant;

            try
            {
                if (criteria.Id == 0)
                {
                    restaurant = new Restaurant();
                    await MapToEntity(restaurant, criteria);
                    restaurant.IsActive = true;
                    UnitOfWorkAsync.RepositoryAsync<Restaurant>().Insert(restaurant);
                }
                else
                {
                    restaurant = await GetRestaurant(criteria.Id);
                    await MapToEntity(restaurant, criteria);
                    UnitOfWorkAsync.RepositoryAsync<Restaurant>().Update(restaurant);
                }

                await UnitOfWorkAsync.SaveChangesAsync();
                UnitOfWorkAsync.Commit();
            }
            catch (Exception e)
            {
                UnitOfWorkAsync.Rollback();
                //if (!e.InnerException.InnerException.GetType().IsAssignableFrom(typeof(SqlException))) throw;
                //var exception = (SqlException)e.InnerException.InnerException;
                //if (exception.Number == 2627)
                //    throw new ApplicationException("Ad Account Id is not unique");
                throw;
            }
            return restaurant.Id;
        }

        public async Task<Rating> RateAsync(Rating rating)
        {
            UnitOfWorkAsync.BeginTransaction();
            try
            {
                UnitOfWorkAsync.RepositoryAsync<Rating>().Insert(rating);
                await UnitOfWorkAsync.SaveChangesAsync();
                UnitOfWorkAsync.Commit();
            }
            catch (Exception e)
            {
                UnitOfWorkAsync.Rollback();
                //if (!e.InnerException.InnerException.GetType().IsAssignableFrom(typeof(SqlException))) throw;
                //var exception = (SqlException)e.InnerException.InnerException;
                //if (exception.Number == 2627)
                //    throw new ApplicationException("Ad Account Id is not unique");
                throw;
            }
            return rating;
        }
        #endregion

        #region Private Helper Methods
        private async Task<bool> MapToEntity(Restaurant restaurant, SaveRestaurantCriteria criteria)
        {
            restaurant.Id = criteria.Id;
            restaurant.Name = criteria.Name;
            restaurant.Street = criteria.Street;
            restaurant.City = criteria.City;
            restaurant.State = criteria.State;
            restaurant.Zip = criteria.Zip;

            restaurant.Cuisines.Clear();

            foreach (var id in criteria.CuisineIds)
            {
                var cuisine = await UnitOfWorkAsync.RepositoryAsync<Cuisine>().FindAsync(id);
                if (cuisine != null)
                    restaurant.Cuisines.Add(cuisine);
            }
            return true;
        }

        private async Task<Restaurant> GetRestaurant(int id)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Restaurant>()
            .Queryable()
            .Include(r => r.Cuisines)
            .Where(r => r.Id == id).SingleOrDefaultAsync();
            return await Task.Run(() => query);
        } 
        #endregion
    }
}
