using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        // TODO GetDiscount from repository and map to CouponModel
        return base.CreateDiscount(request, context);
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken) ?? new Coupon
        {
            ProductName = request.ProductName,
            Description = "No Discount",
            Amount = 0
        };

        return coupon.Adapt<CouponModel>();
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