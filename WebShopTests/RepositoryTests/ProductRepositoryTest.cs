using FakeItEasy;
using WebShop;
using WebShop.Repositories;

namespace WebShopTests.RepositoryTests;

public class ProductRepositoryTest
{
	[Fact]

	public async Task GetProductByIdAsync_ReturnsProduct()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();

		var product = new Product
		{
			Id = 1,
			Name = "Product 1",
			Description = "Description 1",
			Price = 100,
			Stock = 10
		};

		A.CallTo(() => productRepository.GetByIdAsync(product.Id))
			.Returns(Task.FromResult(product)); 

		// Act
		var result = await productRepository.GetByIdAsync(product.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(product.Id, result.Id);
		Assert.Equal(product.Name, result.Name);
		Assert.Equal(product.Description, result.Description);
		Assert.Equal(product.Price, result.Price);
		Assert.Equal(product.Stock, result.Stock);

		A.CallTo(() => productRepository.GetByIdAsync(product.Id))
			.MustHaveHappenedOnceExactly();
	}


	[Fact]

	public async Task GetAllProductsAsync_ReturnsProducts()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var products = new List<Product>
		{
			new Product
			{
				Id = 1,
				Name = "Product 1",
				Description = "Description 1",
				Price = 100,
				Stock = 10
			},
			new Product
			{
				Id = 2,
				Name = "Product 2",
				Description = "Description 2",
				Price = 200,
				Stock = 20
			}
		};


		// Act
		var result = await productRepository.GetAllAsync();

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => productRepository.GetAllAsync()).MustHaveHappenedOnceExactly();

	}

	[Fact]

	public async Task AddProductAsync_AddsProduct()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();
		var product = new Product
		{
			Id = 1,
			Name = "Product 1",
			Description = "Description 1",
			Price = 100,
			Stock = 10
		};

		// Act
		await productRepository.AddAsync(product);

		// Assert
		A.CallTo(() => productRepository.AddAsync(product)).MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task UpdateProductAsync_UpdatesProduct()
	{
		// Arrange
		var productRepository = A.Fake<IProductRepository>();

		var product = new Product
		{
			Id = 1,
			Name = "Product 1",
			Description = "Description 1",
			Price = 100,
			Stock = 10
		};

		// Act
		await productRepository.UpdateAsync(product, 1);

		// Assert
		A.CallTo(() => productRepository.UpdateAsync(product, 1)).MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task DeleteProductAsync_DeletesProduct()
	{
		// Arrange
		
		var productRepository = A.Fake<IProductRepository>();

		// Act
		await productRepository.DeleteAsync(1);

		// Assert
		A.CallTo(() => productRepository.DeleteAsync(1)).MustHaveHappenedOnceExactly();
	}
}