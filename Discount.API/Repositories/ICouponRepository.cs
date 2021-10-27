using Discount.API.Entities;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetByProductName(string productName);
        Task<bool> Save(Coupon coupon);
        Task<bool> Update(Coupon coupon);
        Task<bool> DeleteByProductName(string productName);
    }
}
