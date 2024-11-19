using Microsoft.Extensions.DependencyInjection;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShopSolution.DataAccess;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
		services.AddTransient<INotificationObserver, EmailNotification>();
		services.AddScoped<ProductSubject>();

		return services;
	}
}