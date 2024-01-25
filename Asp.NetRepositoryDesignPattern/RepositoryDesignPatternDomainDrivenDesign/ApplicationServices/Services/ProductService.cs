using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;

namespace RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services
{
    public class ProductService
    {

        #region [Private States]
        private readonly OnlineShopDbContext _onlineShopDbContext;
        private readonly ProductRepository<Product, Guid> _productRepository;
        private readonly Product _product;
        #endregion

        #region [Ctor]
        public ProductService(OnlineShopDbContext onlineShopDbContext)
        {
            _onlineShopDbContext = onlineShopDbContext;
            _productRepository = new ProductRepository<Product, Guid>(_onlineShopDbContext);
            _product = new Product();
        }
        #endregion

        #region [ShowAll()]
        public async Task<List<SelectProductDtoService>> ShowAll()
        {
            var products = await _productRepository.SelectAllAsync();
            var selectProductDtos = new List<SelectProductDtoService>();

            foreach (var product in products)
            {
                var selectProductDtoService = new SelectProductDtoService()
                {
                    Id = product.Id,
                    Title = product.Title,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity,
                };
                selectProductDtos.Add(selectProductDtoService);
            }

            return selectProductDtos;
        }
        #endregion

        #region [Save(InsertProductDtoService insertProductDto)]
        public async Task Save(InsertProductDtoService insertProductDto)
        {
            _product.Id = null;
            _product.Title = insertProductDto.Title;
            _product.UnitPrice = insertProductDto.UnitPrice;
            _product.Quantity = insertProductDto.Quantity;
            await _productRepository.InsertAsync(_product);
        }
        #endregion

        #region [Delete(DeleteProductDtoGetService? deleteProductDtoGetService)]
        public async Task<DeleteProductDtoGetService> Delete(DeleteProductDtoGetService? deleteProductDtoGetService)
        {

            _product.Id = deleteProductDtoGetService.Id;
            return deleteProductDtoGetService;
        }
        #endregion

        #region [DeleteConfirmed(DeleteProductDtoPostService deleteProductDtoPostService)]

        public async Task DeleteConfirmed(DeleteProductDtoPostService deleteProductDtoPostService)
        {
            _product.Id = deleteProductDtoPostService.Id;
            await _productRepository.DeleteAsync(_product);
        }
        #endregion

        #region [ Edit(UpdateProductDtoPostService updateProductDtoPostService)]
        public async Task Edit(UpdateProductDtoPostService updateProductDtoPostService)
        {
            _product.Id = updateProductDtoPostService.Id;
            _product.Title = updateProductDtoPostService.Title;
            _product.UnitPrice = updateProductDtoPostService.UnitPrice;
            _product.Quantity = updateProductDtoPostService.Quantity;
            await _productRepository.UpdateAsync(_product);
        }
        #endregion

        #region [Edit(UpdateProductDtoGetService? updateProductDtoGetService)]

        public async Task<UpdateProductDtoPostService> Edit(UpdateProductDtoGetService? updateProductDtoGetService)
        {
            if (updateProductDtoGetService.Id == null)
            {
                throw new ArgumentNullException(nameof(updateProductDtoGetService.Id));
            }

            var product = await _productRepository.SelectByIdAsync(updateProductDtoGetService.Id);
            if (product == null)
            {
                throw new ArgumentException("product not found");
            }
            var updateProductDtoPostService = new UpdateProductDtoPostService
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity,
            };

            return updateProductDtoPostService;
        }
        #endregion
    }
}
