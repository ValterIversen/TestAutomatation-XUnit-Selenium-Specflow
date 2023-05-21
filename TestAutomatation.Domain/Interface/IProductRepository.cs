using TestAutomatation.Domain.Entities;

namespace TestAutomatation.Domain.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductById(int productID);
    }
}
