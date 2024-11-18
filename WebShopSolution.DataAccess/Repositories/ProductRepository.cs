using WebShop;
using WebShop.Repositories;

namespace WebShopSolution.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
	public async Task<Product> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<Product>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public async Task AddAsync(Product entity)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateAsync(Product entity, int id)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}
}