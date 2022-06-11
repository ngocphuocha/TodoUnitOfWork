using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoUnitOfWork.Core.IConfiguration;
using TodoUnitOfWork.Models;

namespace TodoUnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(ILogger<UsersController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Users.All();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(User user)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }

            return BadRequest();

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _unitOfWork.Users.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            await _unitOfWork.Users.Upsert(user);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var exitsItem = await _unitOfWork.Users.GetById(id);

            if (exitsItem == null) return BadRequest();

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
