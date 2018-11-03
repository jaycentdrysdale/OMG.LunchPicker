using OMG.LunchPicker.Objects.Domain.Criteria;
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
    [RoutePrefix("api/Users")]
    public class UserController : BaseApiController
    {
        #region Private Readonly Fields
        /// <summary>
        /// The service
        /// </summary>
        private readonly IUserService _service;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public UserController(IUserService service)
        {
            _service = service;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        [HttpPost, Route("GetUsers", Name = "UsersRoute")]
        public async Task<IHttpActionResult> GetRestaurants(GetUsersCriteria criteria)
        {
            //if (criteria == null)
            //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.GetAllAsync(criteria);
            return Ok(result);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUser", Name = "SingleUserRoute")]
        public async Task<IHttpActionResult> GetUser(GetByIdCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var user = await _service.GetAsync(criteria);
            if (user == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));

            return Ok(user);
        }

        /// <summary>
        /// Saves the user.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveUser", Name = "SaveUserRoute")]
        public async Task<IHttpActionResult> SaveUser(SaveUserCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.SaveAsync(criteria);
            return Ok(result);
        }

        [HttpPost]
        [Route("AuthenticateUser", Name = "AuthenticateUserRoute")]
        public async Task<IHttpActionResult> AuthenticateUser(LoginCriteria criteria)
        {
            if (criteria == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest));

            var result = await _service.AuthenticateAsync(criteria);
            return Ok(result);
        }

        #endregion
    }
}
