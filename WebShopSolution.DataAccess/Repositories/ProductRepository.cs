using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.Repositories;

namespace WebShopSolution.DataAccess.Repositories;

public class ProductRepository(WebShopSolutionDbContext context) : IProductRepository
{
	public async Task<Product> GetByIdAsync(int id)
	{

		var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

		return product;

		// ev try catch här
	}

	public async Task<IEnumerable<Product>> GetAllAsync()
	{
		var products = await context.Products.ToListAsync();

		return products;
	}

	public async Task AddAsync(Product entity)
	{
		await context.Products.AddAsync(entity);
	}

	public async Task UpdateAsync(Product entity, int id)
	{
		var productToUpdate = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

		if (productToUpdate is null)
		{
			return; // try cath om null returna null
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