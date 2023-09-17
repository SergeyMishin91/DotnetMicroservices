using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProductsAsync();

    Task<Product> GetProductAsync(string id);

    Task<IReadOnlyCollection<Product>> GetProductsByNameAsync(string name);

    Task<IReadOnlyCollection<Product>> GetProductsByCategoryAsync(string category);

    Task CreateProductAsync(Product product);

    Task<bool> UpdateProductAsync(Product product);

    Task<bool> DeleteProductAsync(string id);
}
