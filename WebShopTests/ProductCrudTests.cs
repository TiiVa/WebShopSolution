using FakeItEasy;
using WebShop;
using WebShop.Controllers;
using WebShop.Repositories;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopTests;

public class ProductCrudTests
{
	[Fact]
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

	[Fact]

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

	[Fact]
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
}