using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop;
using WebShopSolution.DataAccess;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopTests
{
	public class UnitOfWorkTests
	{
		[Fact]
		public void NotifyProductAdded_CallsObserverUpdate()
		{
			// Arrange
			var product = new Product { Id = 1, Name = "Test" };

			// Skapar en mock av INotificationObserver
			var mockObserver = new Mock<INotificationObserver>();

			var dbContextOptions = new DbContextOptionsBuilder<WebShopSolutionDbContext>()
			 .UseInMemoryDatabase(databaseName: "TestDb")
			 .Options;

			var dbContext = new WebShopSolutionDbContext(dbContextOptions);

			// Skapar en instans av ProductSubject och l�gger till mock-observat�ren
			var productSubject = new ProductSubject();
			productSubject.Attach(mockObserver.Object);

			// Injicerar v�rt eget ProductSubject i UnitOfWork

			var unitOfWork = new UnitOfWork(dbContext, productSubject);


			// Act
			unitOfWork.NotifyProductAdded(product);

			// Assert
			// Verifierar att Update-metoden kallades p� v�r mock-observat�r
			mockObserver.Verify(o => o.Update(product), Times.Once);
		}
	}


}
