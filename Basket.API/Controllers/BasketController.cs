using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
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
            foreach (var item in shoppingCart.Items)
            {
                var coupon = await _discountGrpcService.GetCouponByProductName(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.Update(shoppingCart));
        }

        [HttpDelete]
        [Route("{userName}")]
        public async Task<ActionResult> DeleteByUserName(string userName)
        {
            await _repository.DeleteByUserName(userName);
            return Ok();
        }
    }
}
