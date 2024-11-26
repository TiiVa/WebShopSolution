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

       
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAllAsync();

            return Ok(products);
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

			if (product is null)
			{
				return NotFound($"No product with id {id}");
			}
			return Ok(product);
		}

		
		[HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]Product product)
        {
	        if (product is null)
	        {
		        return BadRequest("Product is null");
	        }

			try
			{
		       await  _unitOfWork.ProductRepository.AddAsync(product);

				await _unitOfWork.CommitAsync();

		        return Ok("Product added successfully");
	        }
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
			
        }

		[HttpPut("{id}")]

		public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
		{
			if (product == null)
			{
				return BadRequest("Product is null");
			}
			
			try
			{
				await _unitOfWork.ProductRepository.UpdateAsync(product, id);
				
				await _unitOfWork.CommitAsync();

				return Ok("Product updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]

		public async Task<ActionResult> DeleteProduct(int id)
		{
			
			try
			{
				await _unitOfWork.ProductRepository.DeleteAsync(id);
				
				await _unitOfWork.CommitAsync();

				return Ok("Product deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}
