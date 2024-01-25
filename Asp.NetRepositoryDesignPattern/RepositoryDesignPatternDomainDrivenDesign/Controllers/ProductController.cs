using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;

namespace RepositoryDesignPatternDomainDrivenDesign.Controllers
{
    public class ProductController : Controller
    {

        #region [Private State]
        private readonly ApplicationServices.Services.ProductService _productService;
        private readonly Product _product;
        private readonly IMapper _mapper;
        #endregion

        #region [Ctor]
        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _product = new Product();
            _mapper = mapper;
        }

        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoPostController updateProductDtoPostController)]
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductDtoPostController updateProductDtoPostController)
        {
            var updateProductDtoPost = _mapper.Map<UpdateProductDtoPostService>(updateProductDtoPostController);
            await _productService.Edit(updateProductDtoPost);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region [Task<IActionResult> Edit(UpdateProductDtoGetController? updateProductDtoGetController)]
        // GET: products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(UpdateProductDtoGetController? updateProductDtoGetController)
        {
            var updateProductDtoGet = _mapper.Map<UpdateProductDtoGetService>(updateProductDtoGetController);
            var product = await _productService.Edit(updateProductDtoGet);
            var updateProductDtoPostController = _mapper.Map<UpdateProductDtoPostController>(product);
            return View(updateProductDtoPostController);
        }
        #endregion

        #region [Task<IActionResult> Index()]
        // GET: ProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var selectProductDtos = await _productService.ShowAll();
            var selectProductDtoController = new SelectProductDtoController
            {
                Products = _mapper.Map<List<SelectProductDtoService>>(selectProductDtos)
            };

            return View(selectProductDtoController);
        }
        #endregion

        #region [Task<IActionResult> Create()]

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        #endregion

        #region [Task<IActionResult> Create(InsertProductDtoController insertProductDtoController)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProductDtoController insertProductDtoController)
        {
            if (ModelState.IsValid)
            {

                var insertProductDtoGet = _mapper.Map<InsertProductDtoService>(insertProductDtoController);
                await _productService.Save(insertProductDtoGet);
                return RedirectToAction(nameof(Index));
            }
            return View(insertProductDtoController);

        }
        #endregion

        #region [Task<IActionResult> Delete(DeleteProductDtoGetController? deleteProductDtoGetController)]
        // GET:Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(DeleteProductDtoGetController? deleteProductDtoGetController)
        {

            return View(deleteProductDtoGetController);
        }
        #endregion

        #region [Task<IActionResult> DeleteConfirmed(DeleteProductDtoPostController deleteProductDtoPostController)]
        // POST:Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteProductDtoPostController deleteProductDtoPostController)
        {
            var deleteProductDtoPost = _mapper.Map<DeleteProductDtoPostService>(deleteProductDtoPostController);
            await _productService.DeleteConfirmed(deleteProductDtoPost);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}

