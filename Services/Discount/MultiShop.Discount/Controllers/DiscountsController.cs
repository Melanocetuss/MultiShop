using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var values = await _discountService.GetAllDiscountCouponAsync();
            return Ok(values);
        }

        [HttpGet("GetDiscountCouponById")]
        public async Task<IActionResult> GetDiscountCouponById(int CouponID)
        {
            var values = await _discountService.GetByIdDiscountCouponAsync(CouponID);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createDiscountCouponDto)
        {
            await _discountService.CreateDiscountCouponAsync(createDiscountCouponDto);
            return Ok("İndirim Kuponu Ekleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int CouponID)
        {
            await _discountService.DeleteDiscountCouponAsync(CouponID);
            return Ok("İndirim Kuponu Silme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateDiscountCouponDto)
        {
            await _discountService.UpdateDiscountCouponAsync(updateDiscountCouponDto);
            return Ok("İndirim Kuponu Güncelleme İşlemi Başarıyla Gerçekleşti.");
        }

        [HttpGet("GetDiscountCodeDetailByCode/{code}")]
        public async Task<IActionResult> GetDiscountCodeDetailByCode(string code)
        {
            var values = await _discountService.GetDiscountCodeDetailByCodeAsync(code);
            return Ok(values);
        }
    }
}