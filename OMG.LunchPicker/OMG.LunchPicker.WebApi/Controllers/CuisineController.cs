using OMG.LunchPicker.Objects.Domain.Criteria;
using OMG.LunchPicker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OMG.LunchPicker.WebApi.Controllers
{
    [RoutePrefix("api/Cuisine")]
    public class CuisineController: BaseApiController
    {
        #region Private Readonly Fields
        /// <summary>
        /// The service
        /// </summary>
        private readonly ICuisineService _service;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CuisineController(ICuisineService service)
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
        [HttpPost, Route("GetCuisines", Name = "CuisinesRoute")]
        public async Task<IHttpActionResult> GetCuisines(GetCuisinesCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.GetAllAsync(criteria);
            return Ok(result);
        }


        [HttpPost]
        [Route("SaveCuisine", Name = "SaveCuisineRoute")]
        public async Task<IHttpActionResult> SaveCuisine(SaveCuisineCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.SaveAsync(criteria);
            return Ok(result);
        }
        #endregion
    }
}