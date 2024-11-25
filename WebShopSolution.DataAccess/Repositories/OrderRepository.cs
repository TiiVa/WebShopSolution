using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.Repositories;

public class OrderRepository(WebShopSolutionDbContext context) : IOrderRepository
{
	public async Task<Order> GetByIdAsync(int id)
	{
		var order = context.Orders
			.Include(o => o.User)
			.Include(op => op.OrderProducts)
			.FirstOrDefault(o => o.Id == id);

		if(order is null)
		{
			return new Order();
		}

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
		var userForOrder = await context.Users.FirstOrDefaultAsync(u => u.Id == entity.UserId);
		var orderProducts = new List<Product>();

		foreach (var product in entity.OrderProducts)
		{
			var productToAdd = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
			if (productToAdd is not null)
			{
				orderProducts.Add(productToAdd);
			}
		}

		var newOrder = new Order
		{
			OrderDate = entity.OrderDate,
			TotalPrice = entity.TotalPrice,
			OrderProducts = orderProducts,
			IsActive = entity.IsActive,
			UserId = entity.UserId,
			User = userForOrder
		};

		context.Orders.Add(newOrder);

		// For testing in Swagger: 
		// // {
		// //   "userId": 1,
		// // "user" : {
		// // "id" : 1,
		// // "userName" : "Kalle80",
		// // "email" : "kalle@kallesbolag.se",
		// // "password" : "KallesPw",
		// // "isAdmin" : true,
		// // "isActive" : true
		// // },
		// // 
		// //   "orderDate": "2024-11-25T07:27:20.214Z",
		// //   "totalPrice": 500,
		// //   "orderProducts": [
		// //     {
		// //       "id": 1
		// //     }
		// //   ],
		// //   "isActive": true
		// // }
	}
	
	public async Task UpdateAsync(Order entity, int id) // Not implemented
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