using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Repository
{
    public interface IUserRepository
    {
        Task<dynamic> GetAsync(GetByIdCriteria criteria);
        Task<int> SaveAsync(SaveUserCriteria criteria);
        Task<User> AuthenticateAsync(LoginCriteria criteria);
        Task<IQueryable<dynamic>> GetAllAsync(GetUsersCriteria criteria);
    }
}
