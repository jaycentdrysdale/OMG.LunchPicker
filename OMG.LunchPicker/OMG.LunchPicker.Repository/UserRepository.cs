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
    public class UserRepository : DefaultRepository, IUserRepository
    {
        public UserRepository(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync) { }
        public async Task<dynamic> GetAsync(GetByIdCriteria criteria)
        {
            var query = UnitOfWorkAsync.RepositoryAsync<User>()
            .Queryable()
            .Include(u=>u.Ratings)
            .AsNoTracking()
            .Where(u => u.IsActive == criteria.IsActive && u.Id == criteria.Id)
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.EmailAddress,
                u.DateCreated,
                ratings = u.Ratings.Select(r => new {r.Restaurant.Name, r.RatingValue, r.DateCreated })
            })
            .SingleOrDefaultAsync();
            return await Task.Run(() => query);
        }
        public async Task<int> SaveAsync(SaveUserCriteria criteria)
        {
            UnitOfWorkAsync.BeginTransaction();

            User user;

            try
            {
                if (await EmailandUserNameTaken(criteria.Id, criteria.Username, criteria.EmailAddress))
                    throw new ArgumentException("UserName and/or Email already in use. Please try again");

                if (criteria.Id == 0)
                {
                    user = new User();
                    await MapToEntity(user, criteria);
                    user.IsActive = true;
                    UnitOfWorkAsync.RepositoryAsync<User>().Insert(user);
                }
                else
                {
                    user = await UnitOfWorkAsync.RepositoryAsync<User>().FindAsync(criteria.Id);
                    await MapToEntity(user, criteria);
                    UnitOfWorkAsync.RepositoryAsync<User>().Update(user);
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
            return user.Id;
        }
        public async Task<User> AuthenticateAsync(LoginCriteria criteria)
        {
            var user = UnitOfWorkAsync.RepositoryAsync<User>()
            .Queryable()
            .AsNoTracking()
            .Where(r => (r.UserName.Equals(criteria.EmailOrUsername, StringComparison.InvariantCultureIgnoreCase) 
            || r.EmailAddress.Equals(criteria.EmailOrUsername, StringComparison.InvariantCultureIgnoreCase)) 
            //&& r.Password == criteria.Password 
            && r.IsActive == true)
            .FirstOrDefault();

            if (user == null || user.Password != criteria.Password)
                throw new UnauthorizedAccessException($"Login failed for user {criteria.EmailOrUsername}");

            user.Password = string.Empty; // this might have negative effects! Investigate.

            return await Task.Run(() => user);
        }

        private async Task<bool> MapToEntity(User user, SaveUserCriteria criteria)
        {
            user.Id = criteria.Id;
            user.UserName = criteria.Username;
            user.EmailAddress = criteria.EmailAddress;
            user.Password = criteria.Password;
            return true;
        }

        private async Task<bool> EmailandUserNameTaken(int id, string userName, string emailAddress)
        {
            bool userExists = false;

            var user = UnitOfWorkAsync.RepositoryAsync<User>()
            .Queryable()
            .Where(r => r.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) || r.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase))
            .FirstOrDefault();

            if (user != null && user.Id != id)
                userExists = true;

            return await Task.Run(() => userExists);
        }

    }
}
