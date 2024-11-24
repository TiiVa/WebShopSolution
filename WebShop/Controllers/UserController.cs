using Microsoft.AspNetCore.Mvc;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShop.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public UserController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			var users = await _unitOfWork.UserRepository.GetAllAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

			if (user is null)
			{
				return NotFound($"No user with id {id}");
			}
			return Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult> AddUser([FromBody] User user)
		{
			if (user is null)
			{
				return BadRequest("User is null");
			}
			try
			{
				await _unitOfWork.UserRepository.AddAsync(user);

				await _unitOfWork.CommitAsync();

				return Ok("User added successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
		{
			if (user is null)
			{
				return BadRequest("User is null");
			}
			try
			{
				var existingUser = _unitOfWork.UserRepository.GetByIdAsync(id);
				if (existingUser is null)
				{
					return NotFound($"No user with id {id}");
				}
				await _unitOfWork.UserRepository.UpdateAsync(user, id);

			    await _unitOfWork.CommitAsync();

				return Ok("User updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUser(int id)
		{
			var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
			
			if (user is null)
			{
				return NotFound($"No user with id {id}");
			}
			await _unitOfWork.UserRepository.DeleteAsync(id);

			await _unitOfWork.CommitAsync();

			return Ok("User deleted successfully");
		}


	}
}
