using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.Repositories;

public class UserRepository(WebShopSolutionDbContext context) : IUserRepository
{
	public async Task<User> GetByIdAsync(int id)
	{
		var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

		return user;
	}

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		var users = await context.Users.ToListAsync();

		return users;
	}

	public async Task AddAsync(User entity)
	{
		await context.Users.AddAsync(entity);

	}

	public async Task UpdateAsync(User entity, int id)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}
}