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
    public interface ICuisineService
    {
        Task<dynamic> GetAsync(int id);
        Task<MultiItemsResponse<dynamic>> GetAllAsync(PagableCriteriaBase criteria);
        Task<int> SaveAsync(Cuisine cuisine);
    }
}
