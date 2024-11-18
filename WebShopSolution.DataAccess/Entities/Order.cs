namespace WebShop;

public class Order : IEntity<int>
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public User User { get; set; }
	public DateTime OrderDate { get; set; }
	public double TotalPrice { get; set; }
	public List<Product> OrderProducts { get; set; }
	public bool IsActive { get; set; }
}