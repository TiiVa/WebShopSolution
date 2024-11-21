using Moq;
using WebShop.Controllers;
using WebShop.Repositories;

namespace WebShopTests;

public class ProductControllerTests
{
	private readonly Mock<IProductRepository> _mockProductRepository;
	private readonly ProductController _controller;

	public ProductControllerTests()
	{
		_mockProductRepository = new Mock<IProductRepository>();

		// Ställ in mock av Products-egenskapen
	}

	[Fact]
	public void GetProducts_ReturnsOkResult_WithAListOfProducts()
	{
		// Arrange

		// Act

		// Assert
	}

	[Fact]
	public void AddProduct_ReturnsOkResult()
	{
		// Arrange

		// Act

		// Assert
	}
}
//public class ProductControllerTests
//{
//	private readonly Mock<IProductRepository> _mockProductRepository;
//	private readonly Mock<IUnitOfWork> _mockUnitOfWork;
//	private readonly ProductController _controller;

//	public ProductControllerTests()
//	{
//		_mockProductRepository = new Mock<IProductRepository>();
//		_mockUnitOfWork = new Mock<IUnitOfWork>();
//		_controller = new ProductController(_mockUnitOfWork.Object);
//	}

//	[Fact]
//	public async Task GetProducts_ReturnsOkResult_WithAListOfProducts()
//	{
//		// Arrange
//		var products = new List<Product>
//		{
//			new Product { Id = 1, Name = "Product1" },
//			new Product { Id = 2, Name = "Product2" }
//		};
//		_mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

//		// Act
//		var result = await _controller.GetProducts();
//		var okResult = result.Result as OkObjectResult;
//		var returnedProducts = okResult?.Value as List<Product>;

//		// Assert
//		Assert.NotNull(okResult);
//		Assert.NotNull(returnedProducts);
//		Assert.Equal(2, returnedProducts.Count);
//		Assert.IsType<List<Product>>(returnedProducts);
//	}

//	[Fact]
//	public async Task AddProduct_ReturnsOkResult()
//	{
//		// Arrange
//		var product = new Product { Id = 1, Name = "Test Product", Description = "Test Description", Price = 10.0, Stock = 100 };
//		_mockProductRepository.Setup(repo => repo.AddAsync(product));

//		// Act
//		var result = await _controller.AddProduct(product);
//		var okResult = result as OkObjectResult;
//		var returnedProduct = okResult?.Value as Product;

//		// Assert
//		Assert.NotNull(okResult);
//		Assert.Equal(200, okResult.StatusCode);
//		Assert.NotNull(returnedProduct);
//		Assert.Equal(product.Id, returnedProduct.Id);
//		Assert.Equal(product.Name, returnedProduct.Name);
//	}

//}