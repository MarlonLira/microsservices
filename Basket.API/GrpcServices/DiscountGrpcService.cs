using Discount.Grpc.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetCouponByProductName(string productName)
        {
            return await _discountProtoServiceClient.GetAsync(new GetCouponRequest { ProductName = productName });
        }
    }
}
