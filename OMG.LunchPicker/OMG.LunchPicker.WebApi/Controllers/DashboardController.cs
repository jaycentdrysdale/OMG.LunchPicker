using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OMG.LunchPicker.WebApi.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : BaseApiController
    {
        #region Private Readonly Fields
        private readonly IRestaurantService _restaurantService;
        private readonly ICuisineService _cuisineService;
        private readonly IUserService _userService;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public DashboardController(IRestaurantService restaurantService, IUserService userService, ICuisineService cuisineService)
        {
            _restaurantService = restaurantService;
            _userService = userService;
            _cuisineService = cuisineService;
        }
        #endregion

        #region public methods
        [HttpGet, Route("GetDashboard", Name = "DashboardRoute")]
        public async Task<IHttpActionResult> GetDashboard()
        {
            var restaurantResponse = await _restaurantService.GetAllAsync(new GetRestaurantsCriteria()
            {
                Cuisine = null,
                PartialName = null,
                RatingValue = null,
                Reverse = false,
                Skip = 0,
                SortField = null,
                Take = int.MaxValue
            });

            var averageRatingResponse = await _restaurantService.GetAverageRatingAsync();

            var cuisineResponse = await _cuisineService.GetAllAsync(new PagableCriteriaBase()
            {
                Reverse = false,
                Skip = 0,
                Take = int.MaxValue,
                SortField = null
            });

            var usersResponse = await _userService.GetAllAsync(new GetUsersCriteria()
            {
                PartialUserNameOrEmail = null,
                Reverse = false,
                Skip = 0,
                Take = int.MaxValue,
                SortField = null
            });

            var dashboard = new {AverageRating = averageRatingResponse.Result, Users = usersResponse.Results, Restaurants = restaurantResponse.Results, Cuisines = cuisineResponse.Results };
           
            return Ok(dashboard);
        }
        #endregion
    }
}