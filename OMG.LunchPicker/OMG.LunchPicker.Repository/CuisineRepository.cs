using OMG.LunchPicker.Objects.Domain.Criteria;
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
        public async Task<IQueryable<dynamic>> GetAllAsync(GetCuisinesCriteria criteria)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Cuisine>()
            .Queryable()
            .AsNoTracking()
            .Where(c => c.IsActive == criteria.IsActive && string.IsNullOrEmpty(criteria.PartialName) ? true : c.Name.Contains(criteria.PartialName))          
            .OrderBy(r => r.Name);

            return await Task.Run(() => query);
        }

        public async Task<dynamic> GetAsync(GetByIdCriteria criteria)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<Cuisine>()
            .Queryable()
            .AsNoTracking()
            .Where(r => r.IsActive == true && r.Id == criteria.Id)
            .SingleOrDefault();

            return await Task.Run(() => query);
        }

        public async Task<int> SaveAsync(SaveCuisineCriteria criteria)
        {
            UnitOfWorkAsync.BeginTransaction();
            Cuisine cuisine;
            try
            {
                if (criteria.Id == 0)
                {
                    cuisine = new Cuisine();
                    await MapToEntity(cuisine, criteria);
                    cuisine.IsActive = true;
                    UnitOfWorkAsync.RepositoryAsync<Cuisine>().Insert(cuisine);
                }
                else
                {
                    cuisine = await GetAsync(new GetByIdCriteria() { Id = criteria.Id });
                    await MapToEntity(cuisine, criteria);
                    UnitOfWorkAsync.RepositoryAsync<Cuisine>().Update(cuisine);
                }

                await UnitOfWorkAsync.SaveChangesAsync();
                UnitOfWorkAsync.Commit();
            }
            catch (Exception e)
            {
                UnitOfWorkAsync.Rollback();
                throw;
            }
            return cuisine.Id;
        }

        # region Helper Methods
        private async Task<bool> MapToEntity(Cuisine cuisine, SaveCuisineCriteria criteria)
        {
            cuisine.Id = criteria.Id;
            cuisine.Name = criteria.Name;
            return await Task.Run(() => true);
        }
        #endregion

    }
}
