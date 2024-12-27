using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //todo get discount from db
            var coupon = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName) 
                ?? new Models.Coupon { ProductName = "No discount", Amount = 0, Id = 0, Description ="No discount"};
            logger.LogInformation("Discount is retrieved for {ProductName}", request.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if(coupon is not null)
            {
                dbContext.Coupones.Add(coupon);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Discount created for {ProductName}", request.Coupon.ProductName);

            }
            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is not null)
            {
                dbContext.Coupones.Update(coupon);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Discount updated for {ProductName}", request.Coupon.ProductName);

            }
            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is not null)
            {
                dbContext.Coupones.Remove(coupon);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Discount deleted for {ProductName}", request.ProductName);

                return new DeleteDiscountResponse { Sucess = true };
            }
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon not found for {request.ProductName}"));
        }
    }
}
