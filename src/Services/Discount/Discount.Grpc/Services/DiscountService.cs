using Discount.Grpc.Data;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService
    (DiscountContext context, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        // TODO GetDiscount from repository and map to CouponModel
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        return base.GetDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}