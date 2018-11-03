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
        Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria);
        Task<MultiItemsResponse<dynamic>> GetAllAsync(GetCuisinesCriteria criteria = null);
        Task<SingleItemResponse<int>> SaveAsync(SaveCuisineCriteria critera);
    }
}
