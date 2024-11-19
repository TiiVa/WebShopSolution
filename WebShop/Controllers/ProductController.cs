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
    }
}
