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
        Task<dynamic> GetAsync(int id);
        Task<IQueryable<dynamic>> GetAllAsync();
        Task<int> SaveAsync(Cuisine restaurant);
    }
}
