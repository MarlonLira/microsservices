using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Route("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetByUserName(string userName)
        {
            var shoppingCart = await _repository.GetByUserName(userName);

            return Ok(shoppingCart ?? new ShoppingCart(userName));
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ShoppingCart>> Update([FromBody] ShoppingCart shoppingCart)
        {
            return Ok(await _repository.Update(shoppingCart));
        }

        [HttpDelete]
        [Route("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            await _repository.DeleteByUserName(userName);
            return Ok();
        }
    }
}
