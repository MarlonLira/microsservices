using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(ICouponRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<CouponModel> Get(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetByProductName(request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"O Desconto para produto = {request.ProductName} não foi encontrado."));
            }

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> Create(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.Save(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> Update(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.Update(coupon);

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> Delete(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteByProductName(request.ProductName);

            return new DeleteDiscountResponse
            {
                Success = deleted
            };
        }
    }
}
