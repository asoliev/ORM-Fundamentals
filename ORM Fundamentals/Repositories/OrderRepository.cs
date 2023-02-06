using ORM_Fundamentals.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ORM_Fundamentals.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ApplicationDbContext context)
            : base(context) { }

        public IEnumerable<Order> GetOrders(int? month = null, OrderStatus? status = null, int? year = null, int? productId = null)
        {
            var monthParam = month == null ? new SqlParameter("@Month", DBNull.Value) : new SqlParameter("@Month", month);
            var statusParam = status == null ? new SqlParameter("@Status", DBNull.Value) : new SqlParameter("@Status", status);
            var yearParam = year == null ? new SqlParameter("@Year", DBNull.Value) : new SqlParameter("@Year", year);
            var productIdParam = productId == null ? new SqlParameter("@ProductId", DBNull.Value) : new SqlParameter("@ProductId", productId);

            return Context.Orders.FromSqlRaw("sp_FetchOrders @Month, @Year, @Status, @ProductId",
                    monthParam, yearParam, statusParam, productIdParam)
                .ToList();
        }

        public void DeleteOrders(int? month = null, OrderStatus? status = null, int? year = null,
            int? productId = null)
        {
            var monthParam = month == null ? new SqlParameter("@Month", DBNull.Value) : new SqlParameter("@Month", month);
            var statusParam = status == null ? new SqlParameter("@Status", DBNull.Value) : new SqlParameter("@Status", status);
            var yearParam = year == null ? new SqlParameter("@Year", DBNull.Value) : new SqlParameter("@Year", year);
            var productIdParam = productId == null ? new SqlParameter("@ProductId", DBNull.Value) : new SqlParameter("@ProductId", productId);

            var ordersToDelete = Context.Orders.FromSqlRaw("sp_DeleteOrders @Month, @Year, @Status, @ProductId",
                    monthParam, yearParam, statusParam, productIdParam)
                .ToList();

            Context.Orders.RemoveRange(ordersToDelete);
            Context.SaveChanges();
        }
    }
}
