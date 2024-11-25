using System.Text.Json.Serialization;

namespace WebShop;

public class User : IEntity<int>
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? StreetAddress { get; set; }
	public string? City { get; set; }
	public string? ZipCode { get; set; }
	public string? Country { get; set; }
	public bool IsAdmin { get; set; }
	public bool IsActive { get; set; }
	[JsonIgnore]
	public List<Order>? Orders { get; set; }
}