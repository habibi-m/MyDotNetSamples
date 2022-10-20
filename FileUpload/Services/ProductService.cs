using FileUpload.Data;
using FileUpload.Interfaces;
using FileUpload.Models;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product model)
        {
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products
                .Where(c => c.Id == id)
                .Include(p => p.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Include(d => d.Images)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> Update(Product model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
