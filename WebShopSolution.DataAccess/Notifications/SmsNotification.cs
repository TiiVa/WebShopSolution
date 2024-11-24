using WebShop;

namespace WebShopSolution.DataAccess.Notifications;

public class SmsNotification : INotificationObserver
{
	public void Update(Product product)
	{
		Console.WriteLine($"Sms notification: New product added - {product.Name}");
	}
}