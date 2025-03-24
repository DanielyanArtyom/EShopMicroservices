using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger): DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon == null)
            coupon = new Coupon { ProductName = "No discount", Amount = 0, Description = "No discount desc"};

        logger.LogInformation($"Discount is retrieved for Product Name: {coupon.ProductName}, Amount: {coupon.Amount}");
        
        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Created coupon for Product Name: {coupon.ProductName}, with Amount: {coupon.Amount}");
        
        return coupon.Adapt<CouponModel>();
    }
    
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        dbContext.Coupons.Update(coupon);
        
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Updated coupon for Product Name: {coupon.ProductName}, with Amount: {coupon.Amount}");
        
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        
        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon not found for product {request.ProductName}"));
        
        dbContext.Coupons.Remove(coupon);
        
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Removed coupon for Product Name: {coupon.ProductName}");

        return new DeleteDiscountResponse { Success = true };
    }

    
}