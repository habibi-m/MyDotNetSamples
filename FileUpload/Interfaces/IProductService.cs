using FileUpload.Models;

namespace FileUpload.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(int id);
        Task<Product> Add(Product model);
        Task<Product> Update(Product model);
        Task<Product> Delete(int id);
    }
}
