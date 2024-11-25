namespace WebShop
{
    // Gjort properties nullable för att underlätta BDD testning i Swagger
    public class Product : IEntity<int>
	{
        public int Id { get; set; } // Unikt ID för produkten
        public string? Name { get; set; } // Namn på produkten
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        
	}
}