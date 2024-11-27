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
		[Fact]
		public void NotifyProductAdded_CallsObserverUpdate()
		{
			// Arrange
			var mockProductRepository = new Mock<IProductRepository>();
			var mockOrderRepository = new Mock<IOrderRepository>();
			var mockUserRepository = new Mock<IUserRepository>();
			var mockObserver = new Mock<INotificationObserver>();

			// Set up the ProductSubject and attach the observer
			var productSubject = new ProductSubject();
			productSubject.Attach(mockObserver.Object);

			// Create a UnitOfWork instance
			var unitOfWork = new UnitOfWork(context : null,
				productRepository: mockProductRepository.Object,
				userRepository: mockUserRepository.Object,
				orderRepository: mockOrderRepository.Object,
				productSubject: productSubject);

			var product = new Product { Id = 1, Name = "Test" };

			// Act
			unitOfWork.NotifyProductAdded(product);

			// Assert
			mockObserver.Verify(o => o.Update(product), Times.Once);
		}
	}



}
