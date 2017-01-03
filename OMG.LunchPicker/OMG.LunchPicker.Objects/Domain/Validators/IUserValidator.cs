using OMG.LunchPicker.Objects.Domain.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public interface IUserValidator
    {
        Task<bool> ValidateAsync(SaveUserCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages);
        Task<bool> ValidateAsync(LoginCriteria criteria, List<string> messages);
    }
}
