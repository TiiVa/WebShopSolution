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

		// H�r hade det varit bra med en lista p� OrderProducts efter att man lagt till en ny datamodell f�r OrderProduct, samt �ndra fr�n en lista av Product till
		// en lista av OrderProduct i Order-klassen f�r att f� till en junction table mellan Order och Product. Gjorde inte det i detta fallet d� det inte var ett krav.

	}
}