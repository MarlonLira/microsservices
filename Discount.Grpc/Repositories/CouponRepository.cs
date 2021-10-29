using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly NpgsqlConnection _db;

        public CouponRepository(IConfiguration configuration)
        {
            _db = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> DeleteByProductName(string productName)
        {
            var affected = await _db.ExecuteAsync
                    ("DELETE FROM Coupon WHERE ProductName=@ProductName",
                    new { ProductName = productName, });

            return affected > 0;
        }

        public async Task<Coupon> GetByProductName(string productName)
        {
            var coupon = await _db.QueryFirstOrDefaultAsync<Coupon>
                    ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon is null) { return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" }; }

            return coupon;
        }

        public async Task<bool> Save(Coupon coupon)
        {
            var affected = await _db.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount)" +
                    " VALUES (@ProductName, @Description, @Amount)",
                    new
                    {
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount
                    });

            return affected > 0;
        }

        public async Task<bool> Update(Coupon coupon)
        {
            var affected = await _db.ExecuteAsync
                    ("UPDATE Coupon " +
                    " SET ProductName=@ProductName, Description=@Description, Amount=@Amount" +
                    " WHERE Id = @Id",
                    new
                    {
                        Id = coupon.Id,
                        ProductName = coupon.ProductName,
                        Description = coupon.Description,
                        Amount = coupon.Amount
                    });

            return affected > 0;
        }
    }
}
