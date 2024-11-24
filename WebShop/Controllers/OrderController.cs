using Microsoft.AspNetCore.Mvc;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShop.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
		{
			var orders = await _unitOfWork.OrderRepository.GetAllAsync();

			return Ok(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrderById(int id)
		{

			var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

			return Ok(order);
		}

		[HttpPost]

		public async Task<ActionResult> AddOrder([FromBody] Order order)
		{
			if (order is null)
			{
				return BadRequest("Order is null");
			}
			try
			{
				await _unitOfWork.OrderRepository.AddAsync(order);

				await _unitOfWork.CommitAsync();

				return Ok("Order added successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{id}")]

		public async Task<ActionResult> UpdateOrder([FromBody] Order order, int id)
		{
			if (order is null)
			{
				return BadRequest("Order is null");
			}
			try
			{
				await _unitOfWork.OrderRepository.UpdateAsync(order, id);

				await _unitOfWork.CommitAsync();

				return Ok("Order updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			try
			{
				await _unitOfWork.OrderRepository.DeleteAsync(id);

				await _unitOfWork.CommitAsync();

				return Ok("Order deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
