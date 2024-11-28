using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.Repositories;

namespace WebShopSolution.DataAccess.Repositories;

public class ProductRepository(WebShopSolutionDbContext context) : IProductRepository
{
	public async Task<Product> GetByIdAsync(int id)
	{

		var product = context.Products.FirstOrDefault(p => p.Id == id);

		if (product == null)
		{
			return new Product();
		}

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
		var productToUpdate = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

		if (productToUpdate is null)
		{
			return;
		}

		productToUpdate.Name = entity.Name;
		productToUpdate.Description = entity.Description;
		productToUpdate.Price = entity.Price;
		productToUpdate.Stock = entity.Stock;

	}

	public async Task DeleteAsync(int id)
	{
		var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

		if (productToDelete is null)
		{
			return;
		}

		context.Products.Remove(productToDelete);
	}
}