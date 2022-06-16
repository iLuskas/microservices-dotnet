using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();
            return View(products);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(productModel);

                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [Authorize]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var product = await _productService.FindProductsById(id);

            if (product != null) 
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(productModel);

                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(productModel);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var product = await _productService.FindProductsById(id);

            if (product != null)
                return View(product);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.DeleteProduct(productModel.Id);

            if (response) return RedirectToAction(nameof(ProductIndex));            

            return View(productModel);
        }
    }
}
