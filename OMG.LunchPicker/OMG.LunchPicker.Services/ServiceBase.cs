using OMG.LunchPicker.Objects.Domain;
using OMG.LunchPicker.Objects.Domain.Criteria;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMG.LunchPicker.Services
{
    public class ServiceBase
    {
        #region Public Properties
        public List<string> ValidationMessages { get; set; }
        #endregion

        #region Public Methods
        public MultiItemsResponse<T> SuccessResponse<T>(IEnumerable<T> items, PagableCriteriaBase criteria)
        {
            if (!items.Any())
                return new MultiItemsResponse<T>()
                {
                    Total = 0,
                    Results = items,
                    IsSuccess = true,
                    Messages = new List<string>()
                };

            return new MultiItemsResponse<T>()
            {
                Total = items.Count(),
                Results = items.Skip(criteria.Skip).Take(criteria.Take).ToList(),
                IsSuccess = true,
                Messages = new List<string>()
            };
        }

        public SingleItemResponse<T> SuccessResponse<T>(T item)
        {
            return new SingleItemResponse<T>()
            {
                Result = item,
                IsSuccess = true,
                Messages = new List<string>()
            };
        }

        public MultiItemsResponse<T> ErrorResponse<T>(List<string> errors)
        {
            return new MultiItemsResponse<T>()
            {
                Total = 0,
                Results = new List<T>(),
                IsSuccess = false,
                Messages = errors
            };
        }

        public SingleItemResponse<dynamic> ErrorResponse<dynamic>(List<string> errors, bool a = false)
        {
            return new SingleItemResponse<dynamic>()
            {
                Result = default(dynamic),
                IsSuccess = false,
                Messages = errors
            };
        } 
        #endregion
    }
}
