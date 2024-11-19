using WebShop;
using WebShop.Repositories;

namespace WebShopSolution.DataAccess.Repositories;

public class ProductRepository(WebShopSolutionDbContext context) : IProductRepository
{
	public async Task<Product> GetByIdAsync(int id)
	{
		var product = context.Products.FirstOrDefault(p => p.Id == id);

		return product;
	}

	public async Task<IEnumerable<Product>> GetAllAsync()
	{
		var products = context.Products.ToList();

		return products;
	}

	public async Task AddAsync(Product entity)
	{
		var newProduct = new Product
		{

			Name = entity.Name,
			Description = entity.Description,
			Price = entity.Price,
			Stock = entity.Stock
		};

		await context.Products.AddAsync(newProduct);
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