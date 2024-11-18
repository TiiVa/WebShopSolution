namespace WebShop;

public interface IEntity<T>
{
	T Id { get; set; }
}