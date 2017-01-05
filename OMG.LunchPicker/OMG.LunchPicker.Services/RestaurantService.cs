using OMG.LunchPicker.Objects.Domain;
using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Domain.Validators;
using OMG.LunchPicker.Objects.Entities;
using OMG.LunchPicker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Services
{
    public class RestaurantService : ServiceBase, IRestaurantService
    {
        #region Private Fields
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantValidator _validator;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public RestaurantService(IRestaurantRepository repository, IRestaurantValidator validator)
        {
            _repository = repository;
            _validator = validator;

            ValidationMessages = new List<string>();
        }
        #endregion

        #region IRestaurantService Members
        public async Task<MultiItemsResponse<dynamic>> GetAllAsync(GetRestaurantsCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<dynamic>(ValidationMessages);

                var result = await _repository.GetAllAsync(criteria);
                return SuccessResponse(result.ToList(), criteria);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<dynamic>(errors);
            }
        }

        public async Task<SingleItemResponse<int>> GetAverageRatingAsync()
        {
            try
            {
                var result = await _repository.GetAverageRatingAsync();
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<int>(errors, false);
            }
        }

        public async Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<dynamic>(ValidationMessages, true);

                var result = await _repository.GetAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<dynamic>(errors, false);
            }
        }

        public async Task<SingleItemResponse<int>> SaveAsync(SaveRestaurantCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<int>(ValidationMessages, true);

                var result = await _repository.SaveAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<int>(errors, false);
            }
        }

        public async Task<SingleItemResponse<dynamic>> RateAsync(RateRestaurantCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<dynamic>(ValidationMessages, true);

                var result = await _repository.RateAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {

                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<dynamic>(errors, false);
            }
        }
        #endregion
    }
}
