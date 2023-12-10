using AutoMapper;
using BeymenCRUD.Data;
using BeymenCRUD.Data.UserRepo;
using BeymenCRUD.Models.Requests;
using BeymenCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeymenCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public UserController(IUserService userService, IMapper mapper, ICacheService cacheService)
        {
            _userService = userService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? id, string? firstName, string? lastName, int? count, string? userStatus = "all")
        {
            if (id != null)
            {
                var user = _userService.DetailsUser(id);
                return Ok(_mapper.Map<User>(user));
            }

            IQueryable<User> query = _userService.GetUsers();

            query = _userService.UserStatus(query, userStatus);

            query = _userService.TakeUsers(query, count);

            query = _userService.FilterUsers(query, firstName, lastName);

            var result = await query.ToListAsync();

            if (result.Any())
            {
                return Ok(_mapper.Map<IEnumerable<User>>(result));
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserPostRequest userPostRequest)
        {
            var result = await _userService.AddUser(_mapper.Map<User>(userPostRequest));

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<User>(result));
        }
        [HttpPut]
        public async Task<IActionResult> Put(UserPutRequest userPutRequest, string id)
        {
            var result = await _userService.UpdateUser(_mapper.Map<User>(userPutRequest), id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<User>(result));
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = _userService.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
