using OMG.LunchPicker.Objects.Domain;
using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Domain.Validators;
using OMG.LunchPicker.Objects.Entities;
using OMG.LunchPicker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Services
{
    public class CuisineService : ServiceBase, ICuisineService
    {
        #region Private Fields
        /// <summary>
        /// The repository
        /// </summary>
        private readonly ICuisineRepository _repository;
        private readonly ICuisineValidator _validator;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CuisineService(ICuisineRepository repository, ICuisineValidator validator)
        {
            _repository = repository;
            _validator = validator;
            ValidationMessages = new List<string>();
        }
        #endregion

        #region ICuisineService Members
        public async Task<MultiItemsResponse<dynamic>> GetAllAsync(GetCuisinesCriteria criteria = null)
        {
            try
            {
                criteria = await InitCriteria(criteria);
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<dynamic>(ValidationMessages);

                var result = await _repository.GetAllAsync(criteria);
                return SuccessResponse(result, criteria);
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

        public async Task<SingleItemResponse<int>> SaveAsync(SaveCuisineCriteria criteria)
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
        #endregion

        #region Private Methods
        private async Task<GetCuisinesCriteria> InitCriteria(GetCuisinesCriteria criteria)
        {
            if (criteria == null)
            {
                criteria = new GetCuisinesCriteria()
                {
                    IsActive = true,
                    PartialName = null,
                    Reverse = false,
                    Skip = 0,
                    SortField = null,
                    Take = int.MaxValue
                };
            }
            return await Task.Run(() => criteria);
        }
        #endregion

    }
}
