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
    public class UserService : ServiceBase, IUserService
    {
        #region Private Fields
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IUserRepository _repository;
        private readonly IUserValidator _validator;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IUserRepository repository, IUserValidator validator)
        {
            _repository = repository;
            _validator = validator;

            ValidationMessages = new List<string>();
        }
        #endregion

        #region IUserService Members

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public async Task<SingleItemResponse<dynamic>> GetAsync(GetByIdCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<dynamic>(ValidationMessages, false);

                var result = await _repository.GetAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<dynamic>(errors, false);
            }
        }

        public async Task<MultiItemsResponse<dynamic>> GetAllAsync(GetUsersCriteria criteria = null)
        {
            try
            {
                criteria = await InitCriteria(criteria);

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
        public async Task<SingleItemResponse<User>> AuthenticateAsync(LoginCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<User>(ValidationMessages, false);

                var result = await _repository.AuthenticateAsync(criteria);
                return SuccessResponse(result);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>() { ex.Message.ToString() };
                return ErrorResponse<User>(errors, false);
            }
        }

        /// <summary>
        /// Saves the user asynchronously.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        public async Task<SingleItemResponse<int>> SaveAsync(SaveUserCriteria criteria)
        {
            try
            {
                if (await _validator.ValidateAsync(criteria, ValidationMessages) == false)
                    return ErrorResponse<int>(ValidationMessages, false);

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
        private async Task<GetUsersCriteria> InitCriteria(GetUsersCriteria criteria)
        {
            if (criteria == null)
            {
                criteria = new GetUsersCriteria()
                {
                    IsActive = true,
                    PartialUserNameOrEmail = null,
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
