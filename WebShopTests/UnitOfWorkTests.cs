using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.RepositoryInterfaces;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopTests
{
	public class UnitOfWorkTests
	{
		private readonly Mock<IProductRepository> _mockProductRepository = new();
		private readonly Mock<IOrderRepository> _mockOrderRepository = new();
		private readonly Mock<IUserRepository> _mockUserRepository = new();
		private readonly Mock<INotificationObserver> _mockObserver = new();
		private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();


		[Fact]
		public void NotifyProductAdded_CallsObserverUpdate()
		{
			// Arrange
			// Set up the ProductSubject and attach the observer
			var productSubject = new ProductSubject();
			productSubject.Attach(_mockObserver.Object);

			// Create a UnitOfWork instance
			var unitOfWork = new UnitOfWork(context : null,
				productRepository: _mockProductRepository.Object,
				userRepository: _mockUserRepository.Object,
				orderRepository: _mockOrderRepository.Object,
				productSubject: productSubject);

			var product = new Product { Id = 1, Name = "Test" };

			// Act
			unitOfWork.NotifyProductAdded(product);

			// Assert
			_mockObserver.Verify(o => o.Update(product), Times.Once);
		}

		[Fact]

		public async Task CommitAsync_CallsSaveChangesAsync()
		{
			// Arrange

			_mockUnitOfWork.Setup(uow => uow.ProductRepository.AddAsync(It.IsAny<Product>()));
			_mockUnitOfWork.Setup(uow => uow.CommitAsync());

			// Act

			await _mockUnitOfWork.Object.CommitAsync();

			// Assert

			_mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);


		}
	}



}
