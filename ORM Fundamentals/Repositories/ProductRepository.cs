using ORM_Fundamentals.Models;
using System.Data;
using System.Data.SqlClient;

namespace ORM_Fundamentals.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context) { }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.ToList();
        }
    }
}