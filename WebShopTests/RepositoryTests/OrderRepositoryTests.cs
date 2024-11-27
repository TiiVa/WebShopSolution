using FakeItEasy;
using WebShop;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopTests.RepositoryTests;

public class OrderRepositoryTests
{
	[Fact]

	public async Task GetOrderByIdAsync_ReturnsOrder()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var order = new Order
		{
			Id = 1,
			UserId = 1,
			OrderDate = DateTime.UtcNow,
			TotalPrice = 100,
			OrderProducts = new List<Product>
			{
				new Product
				{
					Id = 1,
					Name = "Product 1",
					Description = "Description 1",
					Price = 100,
					Stock = 10
				}
			},
			IsActive = true

		};
		A.CallTo(() => orderRepository.GetByIdAsync(order.Id))
			.Returns(Task.FromResult(order));
		// Act
		var result = await orderRepository.GetByIdAsync(order.Id);
		// Assert
		Assert.NotNull(result);
		Assert.Equal(order.Id, result.Id);
		Assert.Equal(order.UserId, result.UserId);
		Assert.Equal(order.OrderDate, result.OrderDate);
		Assert.Equal(order.TotalPrice, result.TotalPrice);
		Assert.Equal(order.OrderProducts.Count, result.OrderProducts.Count);
		Assert.Equal(order.OrderProducts[0].Id, result.OrderProducts[0].Id);
		Assert.Equal(order.OrderProducts[0].Price, result.OrderProducts[0].Price);
		Assert.Equal(order.OrderProducts[0].Stock, result.OrderProducts[0].Stock);
		Assert.Equal(order.IsActive, result.IsActive);


		A.CallTo(() => orderRepository.GetByIdAsync(order.Id))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task GetAllOrdersAsync_ReturnsOrders()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();

		var orders = new List<Order>
		{
			new Order
			{
				Id = 1,
				UserId = 1,
				OrderDate = DateTime.UtcNow,
				TotalPrice = 100,
				OrderProducts = new List<Product>
				{
					new Product
					{
						Id = 1,
						Name = "Product 1",
						Description = "Description 1",
						Price = 100,
						Stock = 10
					}
				},
				IsActive = true
			},
			new Order
			{
				Id = 2,
				UserId = 2,
				OrderDate = DateTime.UtcNow,
				TotalPrice = 200,
				OrderProducts = new List<Product>
				{
					new Product
					{
						Id = 2,
						Name = "Product 2",
						Description = "Description 2",
						Price = 200,
						Stock = 20
					}
				},
				IsActive = true
			}
		};
		A.CallTo(() => orderRepository.GetAllAsync())
			.Returns(Task.FromResult((IEnumerable<Order>)orders));

		// Act
		var result = await orderRepository.GetAllAsync();
		var resultList = result.ToList();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(orders.Count, resultList.Count);
		Assert.Equal(orders[0].Id, resultList[0].Id);
		Assert.Equal(orders[0].UserId, resultList[0].UserId);
		Assert.Equal(orders[0].OrderDate, resultList[0].OrderDate);
		Assert.Equal(orders[0].TotalPrice, resultList[0].TotalPrice);
		Assert.Equal(orders[0].OrderProducts.Count, resultList[0].OrderProducts.Count);
		Assert.Equal(orders[0].OrderProducts[0].Id, resultList[0].OrderProducts[0].Id);
		Assert.Equal(orders[0].OrderProducts[0].Price, resultList[0].OrderProducts[0].Price);
		Assert.Equal(orders[0].OrderProducts[0].Stock, resultList[0].OrderProducts[0].Stock);
		Assert.Equal(orders[0].IsActive, resultList[0].IsActive);


	}

	[Fact]

	public async Task AddOrderAsync_CallsOrderRepositoryOnce()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var order = new Order
		{
			Id = 1,
			UserId = 1,
			OrderDate = DateTime.UtcNow,
			TotalPrice = 100,
			OrderProducts = new List<Product>
			{
				new Product
				{
					Id = 1,
					Name = "Product 1",
					Description = "Description 1",
					Price = 100,
					Stock = 10
				}
			},
			IsActive = true
		};
		A.CallTo(() => orderRepository.AddAsync(order))
			.Returns(Task.FromResult(order));

		// Act
		await orderRepository.AddAsync(order);

		// Assert
		
		A.CallTo(() => orderRepository.AddAsync(order))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task UpdateOrderAsync_CallsOrderRepositoryOnce()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();

		int id = 1;
		var order = new Order
		{
			Id = 1,
			UserId = 1,
			OrderDate = DateTime.UtcNow,
			TotalPrice = 150,
			OrderProducts = new List<Product>
			{
				new Product
				{
					Id = 1,
					Name = "Product 2",
					Description = "Description 2",
					Price = 150,
					Stock = 9
				}
			},
			IsActive = true
		};

		A.CallTo(() => orderRepository.UpdateAsync(order, 1))
			.Returns(Task.FromResult(order));

		// Act
		await orderRepository.UpdateAsync(order, 1);

		// Assert

		A.CallTo(() => orderRepository.UpdateAsync(order, 1))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]

	public async Task DeleteOrderAsync_CallsOrderRepositoryOnce()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();

		int id = 1;
		var order = new Order
		{
			Id = 1,
			UserId = 1,
			OrderDate = DateTime.UtcNow,
			TotalPrice = 150,
			OrderProducts = new List<Product>
			{
				new Product
				{
					Id = 1,
					Name = "Product 2",
					Description = "Description 2",
					Price = 150,
					Stock = 9
				}
			},
			IsActive = true
		};

		A.CallTo(() => orderRepository.DeleteAsync(id))
			.Returns(Task.FromResult(order));

		// Act
		await orderRepository.DeleteAsync(id);

		// Assert
		A.CallTo(() => orderRepository.DeleteAsync(id))
			.MustHaveHappenedOnceExactly();
	}
}