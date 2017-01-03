using OMG.LunchPicker.Objects.Domain.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public interface IRestaurantValidator
    {
        Task<bool> ValidateAsync(SaveRestaurantCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(GetRestaurantsCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(RateRestaurantCriteria criteria, List<string> messages);
    }
}
