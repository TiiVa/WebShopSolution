using Microsoft.EntityFrameworkCore;
using WebShop;

namespace WebShopSolution.DataAccess;

public class WebShopSolutionDbContext : DbContext
{
	public DbSet<Order> Orders { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<User> Users { get; set; }

	public WebShopSolutionDbContext(DbContextOptions<WebShopSolutionDbContext> options) : base(options)
	{

	}
}