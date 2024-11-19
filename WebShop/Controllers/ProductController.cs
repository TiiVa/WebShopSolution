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

        // Endpoint f�r att h�mta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            // Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkter

            var products = _unitOfWork.ProductRepository.GetAllAsync();

            return Ok(products);
        }

		[HttpGet("{id}")]
		public ActionResult<Product> GetProduct(int id)
		{
			// Beh�ver anv�nda repository via Unit of Work f�r att h�mta produkt
			var product = _unitOfWork.ProductRepository.GetByIdAsync(id);

			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		// Endpoint f�r att l�gga till en ny produkt
		[HttpPost]
        public ActionResult AddProduct([FromBody]Product product)
        {
	        if (product == null)
	        {
		        return BadRequest("Product is null");
	        }

	        // L�gger till produkten via repository

			try
			{
		        _unitOfWork.ProductRepository.AddAsync(product);

		        // Sparar f�r�ndringar

				_unitOfWork.SaveChangesAsync();

		        return Ok("Product added successfully");
	        }
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
	        
			// Notifierar observat�rer om att en ny produkt har lagts till

			
        }

		// Endpoint f�r att uppdatera en produkt

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
				// Sparar f�r�ndringar
				_unitOfWork.SaveChangesAsync();
				return Ok("Product updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		// Endpoint f�r att ta bort en produkt

		[HttpDelete("{id}")]

		public ActionResult DeleteProduct(int id)
		{
			// Tar bort produkten via repository
			try
			{
				_unitOfWork.ProductRepository.DeleteAsync(id);
				// Sparar f�r�ndringar
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
