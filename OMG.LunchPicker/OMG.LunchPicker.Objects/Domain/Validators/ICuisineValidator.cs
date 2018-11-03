using OMG.LunchPicker.Objects.Domain.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public interface ICuisineValidator
    {
        Task<bool> ValidateAsync(SaveCuisineCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(GetCuisinesCriteria criteria, List<string> messages);
    }
}
