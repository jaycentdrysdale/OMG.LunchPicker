using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMG.LunchPicker.Objects.Domain.Criteria;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public class UserValidator : IUserValidator
    {
        public async Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateGet(criteria, messages));
        }

        public async Task<bool> ValidateAsync(SaveUserCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateSave(criteria, messages));
        }

        public async Task<bool> ValidateAsync(LoginCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateLogin(criteria, messages));
        }

        private bool ValidateLogin(LoginCriteria criteria, List<string> messages)
        {
            if (string.IsNullOrWhiteSpace(criteria.EmailOrUsername))
                messages.Add("email address is required");

            if (string.IsNullOrWhiteSpace(criteria.Password))
                messages.Add("password is required");

            return messages.Count == 0;
        }

        private bool ValidateSave(SaveUserCriteria criteria, List<string> messages)
        {
            int passwordlength = 5;
            
            if (string.IsNullOrWhiteSpace(criteria.Username))
                messages.Add("username is required");

            if (string.IsNullOrWhiteSpace(criteria.EmailAddress))
                messages.Add("email address is required");

            if (string.IsNullOrWhiteSpace(criteria.Password))
                messages.Add("password is required");
            else if (criteria.Password.Length < 5)
                messages.Add($"password length must be greater than {passwordlength} characters");

            return messages.Count == 0;
        }
        private bool ValidateGet(GetByIdCriteria criteria, List<string> messages)
        {
            return true;
        }
    }
}
