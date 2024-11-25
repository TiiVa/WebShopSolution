namespace WebShop
{
    // Gjort properties nullable f�r att underl�tta BDD testning i Swagger
    public class Product : IEntity<int>
	{
        public int Id { get; set; } // Unikt ID f�r produkten
        public string? Name { get; set; } // Namn p� produkten
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
        
	}
}