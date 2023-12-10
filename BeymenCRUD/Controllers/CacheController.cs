using BeymenCRUD.Data;
using BeymenCRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BeymenCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var userData = _cacheService.GetData<User>(id);

            if (userData != null)
            {
                return Ok(userData);
            }
            else if (userData == null)
            {
                User dataFromDatabase = _cacheService.GetDataFromDatabase<User>(id);

                if (dataFromDatabase != null)
                {
                    _cacheService.SetData(id, dataFromDatabase, DateTimeOffset.Now.AddSeconds(30));

                    return Ok(dataFromDatabase);
                } 
            }
            return NotFound();
        }
    }
}
