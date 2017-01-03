using OMG.LunchPicker.Objects.Entities;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Repository
{
    public class CuisineRepository : DefaultRepository, ICuisineRepository
    {
        public CuisineRepository(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync) { }
        public async Task<IQueryable<dynamic>> GetAllAsync()
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Cuisine>()
            .Queryable()
            .AsNoTracking()
            .Where(c => c.IsActive == true)          
            .OrderBy(r => r.Name);
            return await Task.Run(() => query);
        }

        public async Task<dynamic> GetAsync(int id)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Cuisine>()
            .Queryable()
            .AsNoTracking()
            .Where(r => r.IsActive == true && r.Id == id)
            .SingleOrDefault();

            return await Task.Run(() => query);
        }

        public async Task<int> SaveAsync(Cuisine cuisine)
        {
            UnitOfWorkAsync.BeginTransaction();
            try
            {

                if (cuisine.Id != 0)
                    UnitOfWorkAsync.RepositoryAsync<Cuisine>().Update(cuisine);
                else
                    UnitOfWorkAsync.RepositoryAsync<Cuisine>().Insert(cuisine);

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
            return cuisine.Id;
        }
    }
}
