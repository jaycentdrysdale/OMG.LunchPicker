using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMG.LunchPicker.Objects.Domain.Criteria;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OMG.LunchPicker.Objects.Domain.Validators
{
    public class RestaurantValidator : IRestaurantValidator
    {
        public async Task<bool> ValidateAsync(GetByIdCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateGetById(criteria, messages));
        }

        public async Task<bool> ValidateAsync(SaveRestaurantCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateSave(criteria, messages));
        }

        public async Task<bool> ValidateAsync(GetRestaurantsCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateGetAll(criteria, messages));
        }

        public async Task<bool> ValidateAsync(RateRestaurantCriteria criteria, List<string> messages)
        {
            return await Task.Run(() => ValidateRateRestaurant(criteria, messages));
        }

        private bool ValidateGetById(GetByIdCriteria criteria, List<string> messages)
        {
            return true;
        }

        private bool ValidateSave(SaveRestaurantCriteria criteria, List<string> messages)
        {
            if (string.IsNullOrWhiteSpace(criteria.Name))
                messages.Add("name is required");

            if (string.IsNullOrWhiteSpace(criteria.Street))
                messages.Add("street is required");

            if (string.IsNullOrWhiteSpace(criteria.City))
                messages.Add("city is required");

            if (string.IsNullOrWhiteSpace(criteria.State))
                messages.Add("state is required");

            if (string.IsNullOrWhiteSpace(criteria.Zip))
                messages.Add("zip code is required");
            else if (!Regex.Match(criteria.Zip, @"^\d{5}(?:[-\s]\d{4})?$").Success)
                messages.Add("zip code is not valid");

            if (criteria.CuisineIds.Count == 0)
                messages.Add("at least one cuisine type is required");

            return messages.Count == 0;
        }

        private bool ValidateGetAll(GetRestaurantsCriteria criteria, List<string> messages)
        {
            return true;
        }
        private bool ValidateRateRestaurant(RateRestaurantCriteria criteria, List<string> messages)
        {
            int maxRating = 5;
            int minRating = 0;

            if(criteria.RatingValue < minRating || criteria.RatingValue > maxRating)
                messages.Add($"rating value must be between {minRating} and {maxRating}");

            return messages.Count == 0;
        }
    }
}
