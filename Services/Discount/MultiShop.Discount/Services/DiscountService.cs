using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;
        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createDiscountCouponDto)
        {
            string query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@Code, @Rate, @IsActive, @ValidDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@Code", createDiscountCouponDto.Code);
            parameters.Add("@Rate", createDiscountCouponDto.Rate);
            parameters.Add("@IsActive", createDiscountCouponDto.IsActive);
            parameters.Add("@ValidDate", createDiscountCouponDto.ValidDate);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int CouponID)
        {
            string query = "DELETE FROM Coupons WHERE CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("@CouponID", CouponID);
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            string query = "SELECT * FROM Coupons";
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int CouponID)
        {
            var query = "SELECT * FROM Coupons WHERE CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("@CouponID", CouponID);
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
                return values;               
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateDiscountCouponDto)
        {
            var query = "UPDATE Coupons SET Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate WHERE CouponID = @CouponID";
            var parameters = new DynamicParameters();
            parameters.Add("@CouponID", updateDiscountCouponDto.CouponID);
            parameters.Add("@Code", updateDiscountCouponDto.Code);
            parameters.Add("@Rate", updateDiscountCouponDto.Rate);
            parameters.Add("@IsActive", updateDiscountCouponDto.IsActive);
            parameters.Add("@ValidDate", updateDiscountCouponDto.ValidDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}