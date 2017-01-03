using OMG.LunchPicker.Objects;
using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Repository
{
    public interface IRestaurantRepository
    {
        Task<dynamic> GetAsync(GetByIdCriteria criteria);
        Task<IQueryable<dynamic>> GetAllAsync(GetRestaurantsCriteria criteria);
        Task<int> SaveAsync(SaveRestaurantCriteria critera);
        Task<Rating> RateAsync(Rating rating);
    }
}
