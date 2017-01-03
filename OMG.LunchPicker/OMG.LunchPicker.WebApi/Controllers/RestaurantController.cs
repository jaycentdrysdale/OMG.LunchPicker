using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Objects.Entities;
using OMG.LunchPicker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OMG.LunchPicker.WebApi.Controllers
{

    [RoutePrefix("api/Restaurants")]
    public class RestaurantController : BaseApiController
    {
        #region Private Readonly Fields
        /// <summary>
        /// The service
        /// </summary>
        private readonly IRestaurantService _service;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Gets the active restaurants.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        [HttpPost, Route("GetRestaurants", Name = "RestaurantsRoute")]
        //public async Task<IHttpActionResult> GetActiveRestaurants(int page = 0, int pageSize = 10, string sortBy = "ID", bool reverse = false)
        public async Task<IHttpActionResult> GetActiveRestaurants(GetRestaurantsCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.GetAllAsync(criteria);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetRestaurant", Name = "SingleRestaurantRoute")]
        //public async Task<IHttpActionResult> GetActiveRestaurants(int page = 0, int pageSize = 10, string sortBy = "ID", bool reverse = false)
        public async Task<IHttpActionResult> GetRestaurant(GetByIdCriteria criteria)
        {
            var restaurant = await _service.GetAsync(criteria);
            if (restaurant == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));

            return Ok(restaurant);
        }

        [HttpPost]
        [Route("SaveRestaurant", Name = "SaveRestaurantRoute")]
        public async Task<IHttpActionResult> SaveRestaurant(SaveRestaurantCriteria critera)
        {
            var result = await _service.SaveAsync(critera);
            return Ok(result);
        }

        [HttpPost]
        [Route("RateRestaurant", Name = "RateRestaurantRoute")]
        //public async Task<IHttpActionResult> GetActiveRestaurants(int page = 0, int pageSize = 10, string sortBy = "ID", bool reverse = false)
        public async Task<IHttpActionResult> RateRestaurant()
        {
            Rating rating = new Rating();
            rating.UserId = 1;
            rating.RestaurantId = 6;
            rating.RatingValue = 5;
            var result = await _service.RateAsync(rating);
            return Ok(result);
        }
        #endregion
    }
}
