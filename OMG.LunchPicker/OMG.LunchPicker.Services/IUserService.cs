using OMG.LunchPicker.Objects.Domain;
using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Services
{
    public interface IUserService
    {
        Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria);
        Task<SingleItemResponse<int>> SaveAsync(SaveUserCriteria criteria);
        Task<SingleItemResponse<User>> AuthenticateAsync(LoginCriteria criteria);
    }
}
