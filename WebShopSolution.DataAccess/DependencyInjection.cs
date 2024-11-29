using Microsoft.Extensions.DependencyInjection;
using WebShop.Repositories;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopSolution.DataAccess;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddTransient<INotificationObserver, EmailNotification>();
		services.AddTransient<INotificationObserver, SmsNotification>();
		services.AddScoped<ProductSubject>();

		// Logger som implementeras i repona

		return services;
	}
}