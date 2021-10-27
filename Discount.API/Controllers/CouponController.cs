using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("product-name/{productName}")]
        public async Task<ActionResult<Coupon>> GetByProductName(string productName)
        {
            return Ok(await this._repository.GetByProductName(productName));
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Coupon>> Save([FromBody] Coupon coupon)
        {
            await this._repository.Save(coupon);
            return await this.GetByProductName(coupon.ProductName);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<bool>> Update([FromBody] Coupon coupon)
        {
            return Ok(await this._repository.Update(coupon));
        }

        [HttpDelete]
        [Route("product-name/{productName}")]
        public async Task<ActionResult<bool>> DeleteByProductName(string productName)
        {
            return Ok(await this._repository.DeleteByProductName(productName));
        }
    }
}
