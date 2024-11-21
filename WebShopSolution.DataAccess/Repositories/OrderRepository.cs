using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.Repositories;

public class OrderRepository(WebShopSolutionDbContext context) : IOrderRepository
{
	public async Task<Order> GetByIdAsync(int id)
	{
		var order = context.Orders.FirstOrDefault(o => o.Id == id);

		return order;
	}

	public async Task<IEnumerable<Order>> GetAllAsync()
	{
		var orders = await context.Orders
			.Include(o => o.User)
			.Include(o => o.OrderProducts).ToListAsync();

		return orders;
	}

	public async Task AddAsync(Order entity)
	{
		var newOrder = new Order
		{
			OrderDate = entity.OrderDate,
			TotalPrice = entity.TotalPrice,
			OrderProducts = entity.OrderProducts,
			IsActive = entity.IsActive,
			UserId = entity.UserId,
			User = entity.User
		};

		context.Orders.Add(newOrder);


	}

	public async Task UpdateAsync(Order entity, int id)
	{
		var orderToUpdate = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

		if (orderToUpdate is null)
		{
			return;
		}

		orderToUpdate.OrderDate = entity.OrderDate;
		orderToUpdate.TotalPrice = entity.TotalPrice;
		orderToUpdate.OrderProducts = entity.OrderProducts;
		orderToUpdate.IsActive = entity.IsActive;
		orderToUpdate.UserId = entity.UserId;
		orderToUpdate.User = entity.User;

	}

	public async Task DeleteAsync(int id)
	{
		var orderToDelete = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);

		if (orderToDelete is null)
		{
			return;
		}

		context.Orders.Remove(orderToDelete);
	}
}