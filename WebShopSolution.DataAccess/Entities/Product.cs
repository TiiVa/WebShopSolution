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

		// Här hade det varit bra med en lista på OrderProducts efter att man lagt till en ny datamodell för OrderProduct, samt ändra från en lista av Product till
		// en lista av OrderProduct i Order-klassen för att få till en junction table mellan Order och Product. Gjorde inte det i detta fallet då det inte var ett krav.

	}
}