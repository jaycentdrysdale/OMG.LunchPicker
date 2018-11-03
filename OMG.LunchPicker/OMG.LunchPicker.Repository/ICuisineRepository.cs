using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Repository
{
    public interface ICuisineRepository
    {
        Task<dynamic> GetAsync(GetByIdCriteria criteria);
        Task<IQueryable<dynamic>> GetAllAsync(GetCuisinesCriteria criteria);
        Task<int> SaveAsync(SaveCuisineCriteria criteria);
    }
}
