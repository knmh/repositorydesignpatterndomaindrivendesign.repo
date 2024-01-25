using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class ProductController : Controller
    {
        #region [Private State]
        private readonly Models.Services.Contracts.IProductRepository<Product, Guid?> _productRepository;
        private readonly Product _product;
        #endregion

        #region [Ctor]
        public ProductController(IProductRepository<Product, Guid?> productRepository)
        {
            _productRepository = productRepository;
            _product = new Product();
        }
        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoPost updateProductDtoPost)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductDtoPost updateProductDtoPost)
        {
            _product.Id = updateProductDtoPost.Id;
            _product.Title = updateProductDtoPost.Title;
            _product.UnitPrice = updateProductDtoPost.UnitPrice;
            _product.Quantity = updateProductDtoPost.Quantity;
            await _productRepository.UpdateAsync(_product);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoGet? updateProductDtoGet)]
        // GET: products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdateProductDtoGet? updateProductDtoGet)
        {
            if (updateProductDtoGet.Id == null)
            {
                throw new ArgumentNullException(nameof(updateProductDtoGet.Id));
            }
            var product = await _productRepository.SelectByIdAsync(updateProductDtoGet.Id);

            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            var updateProductDto = new UpdateProductDtoPost
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity
            };

            return View("Edit", updateProductDto);

        }
        #endregion

        #region [Task<IActionResult> Index()]
        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var products = await _productRepository.SelectAllAsync();
            var SelectProductDtos = new List<SelectProductDto>();

            foreach (var product in products)
            {
                var selectProductDto = new SelectProductDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity,
                };

                SelectProductDtos.Add(selectProductDto);
            }

            return View(SelectProductDtos);

        }
        #endregion

        #region [Task<IActionResult> Create()]

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        #endregion

        #region [Task<IActionResult> Create(InsertProductDto insertProductDto)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProductDto insertProductDto)
        {
            if (ModelState.IsValid)
            {
                _product.Id = null;
                _product.UnitPrice = insertProductDto.UnitPrice;
                _product.Quantity = insertProductDto.Quantity;
                _product.Title = insertProductDto.Title;

                await _productRepository.InsertAsync(_product);
                return RedirectToAction(nameof(Index));
            }
            return View(insertProductDto);

        }
        #endregion

        #region [Task<IActionResult> Delete(DeleteProductDtoGet? deleteProductDtoGet)]
        // GET:Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(DeleteProductDtoGet? deleteProductDtoGet)
        {
            _product.Id = deleteProductDtoGet.Id;
          
            return View(deleteProductDtoGet);
        }
        #endregion

        #region [Task<IActionResult> DeleteConfirmed(DeleteProductDtoPost deleteProductDtoPost)]
        // POST:Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteProductDtoPost deleteProductDtoPost)
        {
            _product.Id = deleteProductDtoPost.Id;
            await _productRepository.DeleteAsync(_product);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
