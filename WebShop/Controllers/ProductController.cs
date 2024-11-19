using Microsoft.AspNetCore.Mvc;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter

            var products = _unitOfWork.ProductRepository.GetAllAsync();

            return Ok(products);
        }

		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			// Behöver använda repository via Unit of Work för att hämta produkt
			var product = _unitOfWork.ProductRepository.GetByIdAsync(id);

			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		// Endpoint för att lägga till en ny produkt
		[HttpPost]
        public ActionResult AddProduct([FromBody]Product product)
        {
	        if (product == null)
	        {
		        return BadRequest("Product is null");
	        }

	        // Lägger till produkten via repository

			try
			{
		        _unitOfWork.ProductRepository.AddAsync(product);

		        // Sparar förändringar

				_unitOfWork.SaveChangesAsync();

		        return Ok("Product added successfully");
	        }
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
	        
			// Notifierar observatörer om att en ny produkt har lagts till

			
        }

		// Endpoint för att uppdatera en produkt

		[HttpPut("{id}")]

		public ActionResult UpdateProduct(int id, [FromBody] Product product)
		{
			if (product == null)
			{
				return BadRequest("Product is null");
			}
			// Uppdaterar produkten via repository
			try
			{
				_unitOfWork.ProductRepository.UpdateAsync(product, id);
				// Sparar förändringar
				_unitOfWork.SaveChangesAsync();
				return Ok("Product updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// Endpoint för att ta bort en produkt

		[HttpDelete("{id}")]

		public ActionResult DeleteProduct(int id)
		{
			// Tar bort produkten via repository
			try
			{
				_unitOfWork.ProductRepository.DeleteAsync(id);
				// Sparar förändringar
				_unitOfWork.SaveChangesAsync();
				return Ok("Product deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
