using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.Repositories;

public class UserRepository(WebShopSolutionDbContext context) : IUserRepository
{
	public async Task<User> GetByIdAsync(int id)
	{
		var user = context.Users.FirstOrDefault(u => u.Id == id);

		return user;
	}

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		var users = context.Users.ToList();

		return users;
	}

	public async Task AddAsync(User entity)
	{

		var newUser = new User
		{
			UserName = entity.UserName,
			Password = entity.Password,
			FirstName = entity.FirstName,
			LastName = entity.LastName,
			Email = entity.Email,
			PhoneNumber = entity.PhoneNumber,
			StreetAddress = entity.StreetAddress,
			City = entity.City,
			ZipCode = entity.ZipCode,
			Country = entity.Country,
			IsAdmin = entity.IsAdmin,
			IsActive = entity.IsActive,
			Orders = new List<Order>()
		};

		await context.Users.AddAsync(newUser);

	}

	public async Task UpdateAsync(User entity, int id)
	{
		var userToUpdate = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

		if (userToUpdate is null)
		{
			return;
		}

		userToUpdate.UserName = entity.UserName;
		userToUpdate.Password = entity.Password;
		userToUpdate.FirstName = entity.FirstName;
		userToUpdate.LastName = entity.LastName;
		userToUpdate.Email = entity.Email;
		userToUpdate.PhoneNumber = entity.PhoneNumber;
		userToUpdate.StreetAddress = entity.StreetAddress;
		userToUpdate.City = entity.City;
		userToUpdate.ZipCode = entity.ZipCode;
		userToUpdate.Country = entity.Country;
		userToUpdate.IsAdmin = entity.IsAdmin;
		userToUpdate.IsActive = entity.IsActive;
		userToUpdate.Orders = entity.Orders;

		
	}

	public async Task DeleteAsync(int id)
	{
		var userToDelete = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

		if (userToDelete is null)
		{
			return;
		}

		context.Users.Remove(userToDelete);
	}
}