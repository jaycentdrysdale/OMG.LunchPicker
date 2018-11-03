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
    public interface IRestaurantService
    {
        Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria);
        Task<MultiItemsResponse<dynamic>> GetAllAsync(GetRestaurantsCriteria criteria = null);
        Task<SingleItemResponse<int>> SaveAsync(SaveRestaurantCriteria critera);
        Task<SingleItemResponse<dynamic>> RateAsync(RateRestaurantCriteria criteria);
        Task<SingleItemResponse<int>> GetAverageRatingAsync();
    }
}
