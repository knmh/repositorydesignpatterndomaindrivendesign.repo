using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories
{
//    public class ProductRepository<T, U> : IProductRepository<Product, Guid?> where T : class
//    {
//        #region [Private States]
//        private readonly OnlineShopDbContext _context;

//        #endregion

//        #region [Ctor]
//        public ProductRepository(OnlineShopDbContext context)
//        {
//            _context = context;
//        }
//        #endregion

//        #region [UpdateAsync(Product product)]
//        public async Task UpdateAsync(Product product)
//        {
//            using (_context)
//            {
//                try
//                {
//                    if (product.Id == null)
//                    {
//                        throw new ArgumentNullException(nameof(product.Id));
//                    }

//                    var existingProduct = await _context.Product.FindAsync(product.Id);
//                    if (existingProduct == null)
//                    {
//                        throw new ArgumentException("product not found", nameof(product.Id));
//                    }

//                    existingProduct.Title = product.Title;
//                    existingProduct.UnitPrice = product.UnitPrice;
//                    existingProduct.Quantity = product.Quantity;

//                    await _context.SaveChangesAsync();
//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//            }
//        }
//        #endregion

//        #region [UpdateAsync(Guid? id)]
//        public async Task UpdateAsync(Guid? id)
//        {
//            using (_context)
//            {
//                try
//                {
//                    if (id == null)
//                    {
//                        throw new ArgumentNullException(nameof(id));
//                    }

//                    var existingProduct = await _context.Product.FindAsync(id);
//                    if (existingProduct == null)
//                    {
//                        throw new ArgumentException("Product not found");
//                    }


//                }
//                catch (Exception)
//                {

//                    throw;
//                }
//                finally
//                {
//                    if (_context != null)
//                    {
//                        _context.Dispose();
//                    }
//                }
//            }

//        }
//        #endregion

//        #region [SelectByIdAsync(Guid? id)]
//        public async Task<Product> SelectByIdAsync(Guid? id)
//        {


//            using (var context = _context)
//            {
//                try
//                {
//                    var existingProduct = await context.Product.FindAsync(id);
//                    return existingProduct;



//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//                finally
//                {
//                    await context.DisposeAsync();
//                }
//            }
//        }
//        #endregion

//        #region [SelectAllAsync()]
//        public async Task<IEnumerable<Product>> SelectAllAsync()
//        {
//            using (_context)
//            {
//                try
//                {

//                    var existingProduct = await _context.Product.ToListAsync();
//                    return existingProduct;
//                }
//                catch (Exception)
//                {

//                    throw;
//                }
//                finally
//                {
//                    if (_context.Product != null) _context.Dispose();
//                }

//            }



//        }
//        #endregion

//        #region [DeleteAsync(Guid? id)]
//        public async Task DeleteAsync(Guid? id)
//        {
//            using (_context)
//            {
//                try
//                {
//                    if (id == null || _context.Product == null)
//                    {
//                        throw new ArgumentException("Product not found");
//                    }

//                    var existingProduct = await (from x in _context.Product
//                                                 where x.Id == id
//                                                 select x).FirstOrDefaultAsync();

//                    if (existingProduct != null)
//                    {
//                        _context.Entry(existingProduct).State = EntityState.Deleted;
//                        await _context.SaveChangesAsync();
//                    }


//                }
//                catch (Exception)
//                {
//                    throw;
//                }
//                finally
//                {
//                    if (_context.Product != null) _context.Dispose();
//                }
//            }
//        }
//        #endregion

//        #region [DeleteAsync(Product product)]
//        public async Task DeleteAsync(Product product)
//        {
//            using (_context)
//            {

//                try
//                {
//                    if (_context.Product == null)
//                    {
//                        throw new ArgumentException("Entity set 'ProductIdentityDBContext.Product'  is null.");
//                    }
//                    var existingProduct = await _context.Product.FindAsync(product.Id);
//                    if (existingProduct != null)
//                    {
//                        _context.Product.Remove(existingProduct);
//                    }
//                    await _context.SaveChangesAsync();
//                }
//                catch (Exception)
//                {

//                    throw;
//                }
//            }
//        }
//        #endregion

//        #region [InsertAsync(Product product)]
//        public async Task InsertAsync(Product product)
//        {
//            using (_context)
//            {
//                try
//                {
//                    var existingProduct = await _context.Product.AddAsync(product);
//                    await _context.SaveChangesAsync();

//                }
//                catch (Exception)
//                {

//                    throw;
//                }
//                finally
//                {
//                    if (_context.Product != null) _context.Dispose();
//                }
//            }
//        }
//        #endregion
//    }
}
