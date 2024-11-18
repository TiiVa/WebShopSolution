using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
	public async Task<Order> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Order>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public async Task AddAsync(Order entity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(Order entity, int id)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}
}