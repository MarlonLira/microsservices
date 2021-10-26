using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public CatalogController(IProductRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _repository.GetById(id);
            if (product is null) { return NotFound(); }

            return Ok(product);
        }

        [HttpGet]
        [Route("category/{category}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
        {
            if (category is null) { return BadRequest("Invalid Category"); }

            var product = await _repository.GetByCategory(category);
            if (product is null) { return NotFound(); }

            return Ok();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> Save([FromBody] Product product)
        {
            if (product is null) { return BadRequest("Invalid product"); }

            product.Id = ObjectId.GenerateNewId().ToString();

            await _repository.Save(product);

            return CreatedAtRoute("", new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] Product product)
        {
            if (product is null) { return BadRequest("Invalid product"); }

            return Ok(await _repository.Update(product));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
