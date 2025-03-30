using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product model)
        {
            await _service.PostAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Product model)
        {
            await _service.PutAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {

            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetAllParents")]
        public async Task<IActionResult> GetAllParents()
        {
            var result = await _service.GetAllParentsAsync();
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination([FromQuery] int offset, [FromQuery] int limit)
        {
            var result = await _service.GetPaginationAsync(offset, limit);
            return Ok(result);
        }
    }
}
