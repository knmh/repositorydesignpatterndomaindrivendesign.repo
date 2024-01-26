using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class ProductController : Controller
    {

        #region [Private State]
        private readonly ApplicationServices.Services.ProductService _productService;
        private readonly Product _product;
  
        #endregion

        #region [Ctor]
        public ProductController(ProductService productService)
        {
            _productService = productService;
            _product = new Product();
        
        }

        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoPost updateProductDtoPost)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductDtoPostService updateProductDtoPostService)
        {
            await _productService.Edit(updateProductDtoPostService);
            return RedirectToAction(nameof(Index));
         }

        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoGet? updateProductDtoGet)]
        // GET: products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdateProductDtoGetService? updateProductDtoGetService)
        {
            var product = await _productService.Edit(updateProductDtoGetService);
            return View(product);
           
        }
        #endregion

        #region [Task<IActionResult> Index()]
        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
        
            var selectProductDtos = await _productService.ShowAll();
            return View(selectProductDtos);
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
        public async Task<IActionResult> Create(InsertProductDtoService insertProductDtoService)
        {
            if (ModelState.IsValid)
            {

                await _productService.Save(insertProductDtoService);
                return RedirectToAction(nameof(Index));
            }
            return View(insertProductDtoService);


        }
        #endregion

        #region [Task<IActionResult> Delete(DeleteProductDtoGet? deleteProductDtoGet)]
        // GET:Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(DeleteProductDtoGetService? deleteProductDtoGetService)
        {

            return View(deleteProductDtoGetService);
        }
        #endregion

        #region [Task<IActionResult> DeleteConfirmed(DeleteProductDtoPost deleteProductDtoPost)]
        // POST:Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteProductDtoPostService deleteProductDtoPostService)
        {
            await _productService.DeleteConfirmed(deleteProductDtoPostService);
            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}


