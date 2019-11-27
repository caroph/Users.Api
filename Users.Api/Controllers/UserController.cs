using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Users.Domain.ObjectValue;
using Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            ActionResult<IEnumerable<UserResponse>> actionResult;

            try
            {
                var result = await _userService.GetAll();
                actionResult = result.Success ? Ok(result.Data) : 
                    (ActionResult<IEnumerable<UserResponse>>)NotFound();
            }
            catch (Exception ex)
            {
                actionResult = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return actionResult;
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<UserResponse>> GetById(string guid)
        {
            ActionResult<UserResponse> actionResult;

            try
            {
                var result = await _userService.Get(guid);
                actionResult = result.Success ? Ok(result.Data) : 
                    (ActionResult<UserResponse>)NotFound();
            }
            catch (Exception ex)
            {
                actionResult = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return actionResult;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post([FromBody] UserRequest user)
        {
            ActionResult<UserResponse> actionResult;
            try
            {
                var result = await _userService.Save(user);
                if (result.Success)
                {
                    var uri = $"/user/{result.Data.Guid}";
                    actionResult = Created(uri, result.Data);
                }
                else actionResult = StatusCode((int)HttpStatusCode.InternalServerError, result.Message);
            }
            catch (Exception ex)
            {
                actionResult = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return actionResult;
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<UserResponse>> PutAsync(string guid, [FromBody] UserRequest user)
        {
            ActionResult<UserResponse> actionResult;
            try
            {
                var result = await _userService.Update(user, guid);
                if (result.Success)
                {
                    var uri = $"/user/{result.Data.Guid}";
                    actionResult = Created(uri, result.Data);
                }
                else actionResult = StatusCode((int)HttpStatusCode.InternalServerError, result.Message);
            }
            catch (Exception ex)
            {
                actionResult = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return actionResult;
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(string guid)
        {
            IActionResult actionResult;
            try
            {
                var result = await _userService.Delete(guid);
                actionResult = result.Success ? Ok(result.Data) : 
                    StatusCode((int)HttpStatusCode.InternalServerError, result.Message);
            }
            catch (Exception ex)
            {
                actionResult = StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

            return actionResult;
        }
    }
}
