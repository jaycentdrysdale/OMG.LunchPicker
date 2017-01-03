using OMG.LunchPicker.Objects.Domain;
using OMG.LunchPicker.Objects.Domain.Criteria;
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
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region IRestaurantService Members
        public async Task<MultiItemsResponse<dynamic>> GetAllAsync(GetRestaurantsCriteria criteria)
        {
            try
            {
                var result = await _repository.GetAllAsync(criteria);
                return SuccessResponse(result.ToList(), criteria);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<dynamic>(errors);
            }
        }

        public async Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria)
        {
            try
            {
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
                

                var result = await _repository.SaveAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<int>(errors, false);
            }
        }

        public async Task<SingleItemResponse<Rating>> RateAsync(Rating rating)
        {
            try
            {
                var result = await _repository.RateAsync(rating);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {

                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<Rating>(errors, false);
            }
        }
        #endregion
    }
}
