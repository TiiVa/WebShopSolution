using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebShop;
using WebShop.Controllers;
using WebShopSolution.DataAccess.RepositoryInterfaces;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopTests;

public class OrderCrudTests
{
	[Fact]
	public async Task GetAllOrders_ReturnsOkResult()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);

		// Act
		var result = await controller.GetAllOrders();

		// Assert
		
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.GetAllAsync()).Returns(Task.FromResult<IEnumerable<Order>>(new List<Order>()));
	}

	[Fact]
	public void GetOrderById_ReturnsOkResultAndObject() // TODO: Kolla om detta är SRP
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);

		int id = 1;
		var order = new Order
		{
			Id = 1,
			OrderDate = DateTime.Now,
			TotalPrice = 100,
			IsActive = true,
			UserId = 1
		};

		// Act
		var result = controller.GetOrderById(1);

		// Assert
		Assert.Equal(order.Id, id);
		A.CallTo(() => orderRepository.GetByIdAsync(1)).Returns(order);
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult(order));
	}

	[Fact]
	public void GetOrderById_ReturnsNotFoundResult()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);

		// Act
		var result = controller.GetOrderById(1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.GetByIdAsync(A<int>.Ignored)).Returns(Task.FromResult<Order>(null));
	}

	
	[Fact]
	public void AddOrder_ReturnsOkResult()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);

		var order = new Order
		{
			Id = 1,
			OrderDate = DateTime.Now,
			TotalPrice = 100,
			IsActive = true,
			UserId = 1
		};

		// Act
		var result = controller.AddOrder(order);

		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.AddAsync(A<Order>.Ignored)).Returns(Task.CompletedTask);

	}

	[Fact]
	public void UpdateOrder_ReturnsOkResult()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);
		var order = new Order
		{
			Id = 1,
			OrderDate = DateTime.Now,
			TotalPrice = 100,
			IsActive = true,
			UserId = 1
		};

		// Act
		var result = controller.UpdateOrder(order, 1);

		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.UpdateAsync(A<Order>.Ignored, A<int>.Ignored)).Returns(Task.CompletedTask);
	}

	[Fact]
	public void DeleteOrder_ReturnsOkResult()
	{
		// Arrange
		var orderRepository = A.Fake<IOrderRepository>();
		var unitOfWork = A.Fake<IUnitOfWork>();
		var controller = new OrderController(unitOfWork);
		var order = new Order
		{
			Id = 1,
			OrderDate = DateTime.Now,
			TotalPrice = 100,
			IsActive = true,
			UserId = 1
		};
		
		// Act
		var result = controller.DeleteOrder(1);
		
		// Assert
		Assert.NotNull(result);
		A.CallTo(() => unitOfWork.OrderRepository).Returns(orderRepository);
		A.CallTo(() => orderRepository.DeleteAsync(A<int>.Ignored)).Returns(Task.CompletedTask);
	}

	
}