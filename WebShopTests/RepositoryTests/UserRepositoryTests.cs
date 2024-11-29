using FakeItEasy;
using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopTests.RepositoryTests;

public class UserRepositoryTests
{
	[Fact]

	public async Task GetUserByIdAsync_ReturnsUser()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();

		var user = new User
		{
			Id = 1,
			FirstName = "User 1",
			LastName = "User 1",
			Email = "user@user.com",
			Password = "password",
			UserName = "user",
			PhoneNumber = "123456789",
			StreetAddress = "Street 1",
			City = "City 1",
			ZipCode = "12345",
			Country = "Sweden",
			IsAdmin = false,
			IsActive = true
		};

		A.CallTo(() => userRepository.GetByIdAsync(user.Id))
			.Returns(Task.FromResult(user));

		// Act

		var result = await userRepository.GetByIdAsync(1);

		//Assert
		Assert.NotNull(result);
		Assert.Equal(user.Id, result.Id);
		Assert.Equal(user.FirstName, result.FirstName);
		Assert.Equal(user.LastName, result.LastName);
		Assert.Equal(user.Email, result.Email);
		Assert.Equal(user.Password, result.Password);
		Assert.Equal(user.UserName, result.UserName);
		Assert.Equal(user.PhoneNumber, result.PhoneNumber);
		Assert.Equal(user.StreetAddress, result.StreetAddress);
		Assert.Equal(user.City, result.City);
		Assert.Equal(user.ZipCode, result.ZipCode);
		Assert.Equal(user.Country, result.Country);
		Assert.Equal(user.IsAdmin, result.IsAdmin);
		Assert.Equal(user.IsActive, result.IsActive);

		A.CallTo(() => userRepository.GetByIdAsync(user.Id)).MustHaveHappenedOnceExactly();

	}

	[Fact]
	public async Task GetAllUsersAsync_ReturnsUsers()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>(); // Assuming IUserRepository exists
		var users = new List<User>
		{
			new User
			{
				Id = 1,
				UserName = "User 1",
				Email = "user1@example.com"

			},
			new User
			{
				Id = 2,
				UserName = "User 2",
				Email = "user2@example.com"

			}
		};

		A.CallTo(() => userRepository.GetAllAsync())
			.Returns(Task.FromResult(
				(IEnumerable<User>)users)); // Cast till IEnumerable<User> så att kan testa resultat med index

		// Act
		var result = await userRepository.GetAllAsync();

		// Assert
		Assert.NotNull(result);

		var resultList = result.ToList();
		Assert.Equal(users.Count, resultList.Count);
		Assert.Equal(users[0].Id, resultList[0].Id);
		Assert.Equal(users[0].UserName, resultList[0].UserName);
		Assert.Equal(users[0].Email, resultList[0].Email);
		Assert.Equal(users[1].Id, resultList[1].Id);
		Assert.Equal(users[1].UserName, resultList[1].UserName);
		Assert.Equal(users[1].Email, resultList[1].Email);

		A.CallTo(() => userRepository.GetAllAsync())
			.MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task AddUserAsync_CallsUserRepositoryOnce()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var user = new User
		{
			Id = 1,
			FirstName = "User 1",
			LastName = "User 1",
			Email = "testuser@user.com",
			Password = "password",
			UserName = "testUser",
			PhoneNumber = "123456789",
			StreetAddress = "Street 1",
			City = "City 1",
			ZipCode = "12345",
			Country = "Sweden",
			IsAdmin = false,
			IsActive = true

		};

		// Act

		await userRepository.AddAsync(user);

		// Assert

		A.CallTo(() => userRepository.AddAsync(user)).MustHaveHappenedOnceExactly();

	}

	[Fact]

	public async Task UpdateUserAsync_UpdatesUser()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();

		var user = new User
		{
			Id = 1,
			UserName = "UpdatedUser",
			Password = "NewPassW",
			FirstName = "UpdName",
			LastName = "UpdLastName",
			Email = "new@new.com",
			PhoneNumber = "09876543321",
			StreetAddress = "New street",
			City = "New city",
			ZipCode = "123 45",
			Country = "Finland",
			IsAdmin = true,
			IsActive = true,
			Orders = new List<Order>()
		};

		// Act
		await userRepository.UpdateAsync(user, 1);

		// Assert
		A.CallTo(() => userRepository.UpdateAsync(user, 1)).MustHaveHappenedOnceExactly();
	}


	[Fact]

	public async Task DeleteUserAsync_CallsUserRepositoryOnce()
	{
		// Arrange

		var userRepository = A.Fake<IUserRepository>();

		// Act

		await userRepository.DeleteAsync(1);

		// Assert

		A.CallTo(() => userRepository.DeleteAsync(1)).MustHaveHappenedOnceExactly();
	}





}
