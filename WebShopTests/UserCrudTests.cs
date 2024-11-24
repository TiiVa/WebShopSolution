using FakeItEasy;
using WebShop;
using WebShop.Controllers;
using WebShopSolution.DataAccess.RepositoryInterfaces;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopTests;

public class UserCrudTests
{
	[Fact]
	public void GetAllUsers_ReturnsOkResult()
	{

		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);

		// Act
		var result = controller.GetUsers();

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => unitOfWork.UserRepository.GetAllAsync())
			.Returns(Task.FromResult<IEnumerable<User>>(new List<User>()));
	}

	[Fact]
	public void GetUserById_ReturnsOkResult()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);
		var user = new User
		{
			Id = 1,
			FirstName = "Test",
			LastName = "Test",
			Email = "test@test.com",
			Password = "test",
			UserName = "TestUser",
			StreetAddress = "Street 3",
			ZipCode = "12345",
			City = "City",
			Country = "Country",
			PhoneNumber = "1234567890"


		};

		// Act

		var result = controller.GetUserById(1);

		// Assert

		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => userRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult(user));

	}

	[Fact]
	public void GetUserById_ReturnsNotFoundResult()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);

		// Act
		var result = controller.GetUserById(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => userRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult<User>(null));
	}

	[Fact]
	public void AddUser_ReturnsOkResult()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);
		var user = new User
		{
			Id = 1,
			FirstName = "Test",
			LastName = "Test",
			Email = "test@test.com",
			Password = "test",
			UserName = "TestUser",
			StreetAddress = "Street 3",
			ZipCode = "12345",
			City = "City",
			Country = "Country",
			PhoneNumber = "1234567890"
		};

		// Act
		var result = controller.AddUser(user);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => userRepository.AddAsync(A<User>.Ignored)).Returns(Task.CompletedTask);

	}

	[Fact]
	public void UpdateUser_ReturnsOkResult()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);
		var user = new User
		{
			Id = 1,
			FirstName = "TestUpd",
			LastName = "TestUpd",
			Email = "TestUpd@test.com",
			Password = "TestUpd",
			UserName = "TestUpd",
			StreetAddress = "TestUpd 3",
			ZipCode = "12345",
			City = "NewCity",
			Country = "NewCountry",
			PhoneNumber = "0987654321"
		};

		// Act
		var result = controller.UpdateUser(1, user);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => userRepository.UpdateAsync(A<User>.Ignored, A<int>.Ignored)).Returns(Task.CompletedTask);

	}

	[Fact]
	public void DeleteUser_ReturnsOkResult()
	{
		// Arrange
		var userRepository = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new UserController(unitOfWork);

		// Act
		var result = controller.DeleteUser(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.UserRepository).Returns(userRepository);
		A.CallTo(() => userRepository.DeleteAsync(A<int>.Ignored)).Returns(Task.CompletedTask);
	}

}