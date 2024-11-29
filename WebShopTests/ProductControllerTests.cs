using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebShop;
using WebShop.Controllers;
using WebShop.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;
using WebShopSolution.DataAccess.UnitOfWork;
using Times = Moq.Times;

namespace WebShopTests;

public class ProductControllerTests
{
	private readonly Mock<IUnitOfWork> _mockUnitOfWork;
	private readonly ProductController _controller;
	

	public ProductControllerTests()
	{
		_mockUnitOfWork = new Mock<IUnitOfWork>();
		_controller = new ProductController(_mockUnitOfWork.Object);
	}

	[Fact] // FakeItEasy
	public void GetAllProducts_ReturnsOkResult()
	{

		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		// Act
		var result = controller.GetProducts();

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => unitOfWork.ProductRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

	}

	[Fact] // Moq
	public async Task AddProduct_ValidProduct_ReturnsOkResult()
	{
		// Arrange
		var product = new Product
		{
			Id = 1,
			Name = "Test Product",
			Price = 20,
			Stock = 10,
		};

		_mockUnitOfWork.Setup(uow => uow.ProductRepository.AddAsync(It.IsAny<Product>()));
		_mockUnitOfWork.Setup(uow => uow.CommitAsync());

		// Act
		var result = await _controller.AddProduct(product);

		// Assert

		Assert.NotNull(result);
		Assert.IsType<OkObjectResult>(result);
		Assert.IsNotType<BadRequestResult>(result);
		Assert.IsNotType<NotFoundResult>(result);

	}


	[Fact] // FakeItEasy
	public void AddProduct_ReturnsOkResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		var product = new Product
		{
			Id = 1,
			Name = "Test",
			Description = "Test",
			Price = 100,
			Stock = 10
		};

		// Act
		var result = controller.AddProduct(product);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.AddAsync(A<Product>.Ignored)).Returns(Task.CompletedTask);
	}



	[Fact]
	public async Task AddProduct_InvalidProduct_ReturnsBadRequest()
	{
		// Arrange
		var product = new Product
		{
			Name = "", // Invalid name
			Price = -5, // Invalid price
			Stock = -1 // Invalid stock
		};

		// Act
		var result = await _controller.AddProduct(product);

		// Assert
		var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
		Assert.Equal("Product is null", badRequestResult.Value);
		
	}


	[Fact] // FakeItEasy
	public void GetProductById_ReturnsNotFoundResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		// Act
		var result = controller.GetProductById(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult<Product>(null));
	}

	[Fact] // FakeItEasy

	public void GetProductById_ReturnsOkResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		int id = 1;
		var product = new Product
		{
			Id = 1,
			Name = "Test",
			Description = "Test",
			Price = 100,
			Stock = 10
		};

		// Act
		var result = controller.GetProductById(1);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(product.Id, id);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult(product));
	}

	[Fact]
	public async Task GetProductById_ExistingId_ReturnsOkResult()
	{
		// Arrange
		var product = new Product
		{
			Id = 1,
			Name = "Test Product",
			Price = 20.5,
			Stock = 10
		};

		_mockUnitOfWork.Setup(uow => uow.ProductRepository.GetByIdAsync(1)).ReturnsAsync(product);

		// Act
		var result = await _controller.GetProductById(1);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<ActionResult<Product>>(result);


	}

	[Fact]
	public async Task GetProductById_CallsProductRepository_DoesNotCallUserRepository()	
	{
		// Arrange
		var productRepo = A.Fake<IProductRepository>();
		var userRepo = A.Fake<IUserRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();

		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepo);

		var controller = new ProductController(unitOfWork);

		// Act
		var result = await controller.GetProductById(1);

		// Assert
		A.CallTo(() => productRepo.GetByIdAsync(1)).MustHaveHappenedOnceExactly();
		A.CallTo(() => userRepo.GetByIdAsync(1)).MustNotHaveHappened();


	}


	[Fact]
	public void UpdateProduct_ReturnsOkResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		var product = new Product
		{
			Id = 1,
			Name = "Testing",
			Description = "Test",
			Price = 100,
			Stock = 10
		};

		// Act
		var result = controller.UpdateProduct(1, product);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.UpdateAsync(A<Product>.Ignored, A<int>.Ignored)).Returns(Task.CompletedTask);
	}

	[Fact]
	public void UpdateProduct_ReturnsNotFoundResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		var product = new Product
		{
			Id = 1,
			Name = "Test",
			Description = "Test",
			Price = 100,
			Stock = 10
		};

		// Act
		var result = controller.UpdateProduct(1, product);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.UpdateAsync(A<Product>.Ignored, A<int>.Ignored)).Returns(Task.FromResult<Product>(null));
	}

	[Fact]
	public void DeleteProduct_ReturnsOkResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		// Act
		var result = controller.DeleteProduct(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.DeleteAsync(A<int>.Ignored)).Returns(Task.CompletedTask);
	}

	[Fact]
	public void DeleteProduct_ReturnsNotFoundResult()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new ProductController(unitOfWork);

		// Act
		var result = controller.DeleteProduct(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.ProductRepository).Returns(productRepository);
		A.CallTo(() => productRepository.DeleteAsync(A<int>.Ignored)).Returns(Task.FromResult<Product>(null));
	}
}