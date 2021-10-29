using System.Threading.Tasks;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetByProductName(string productName);
        Task<bool> Save(Coupon coupon);
        Task<bool> Update(Coupon coupon);
        Task<bool> DeleteByProductName(string productName);
    }
}
