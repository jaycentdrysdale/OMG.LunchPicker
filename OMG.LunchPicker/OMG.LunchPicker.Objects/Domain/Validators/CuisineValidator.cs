using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMG.LunchPicker.Objects.Domain.Criteria;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public class CuisineValidator : ICuisineValidator
    {
        #region Public Methods
        public async Task<bool> ValidateAsync(GetCuisinesCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateGetAll(criteria, messages));
        }

        public async Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateGetById(criteria, messages));
        }

        public async Task<bool> ValidateAsync(SaveCuisineCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateSave(criteria, messages));
        }
        #endregion

        #region private Methods
        private bool ValidateGetById(GetByIdCriteria criteria, List<string> messages)
        {
            return criteria.Id >= 0;
        }

        private bool ValidateGetAll(GetCuisinesCriteria criteria, List<string> messages)
        {
            return true;
        }

        private bool ValidateSave(SaveCuisineCriteria criteria, List<string> messages)
        {
            if (criteria.Id < 0)
                messages.Add("id must be a positive number");

            if (string.IsNullOrWhiteSpace(criteria.Name))
                messages.Add("name is required");

            return messages.Count == 0;
        }
        #endregion    
    }

}
