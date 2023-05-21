using System.Linq;
using TestAutomatation.Domain.Entities;
using TestAutomatation.Domain.Interface;
using TestAutomatation.Infra.Data.Context;

namespace TestAutomatation.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context.AppContext context)
        : base(context)
        {
        }

        public Product GetProductById(int productID)
        {
            return DbSet.FirstOrDefault(c => c.ID == productID);
        }
    }
}