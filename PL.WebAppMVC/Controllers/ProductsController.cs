﻿using System.Threading.Tasks;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using DAL.EF.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PL.WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }


        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _productService.GetSelectedCategoryNames());
            ViewData["SupplierId"] = new SelectList(await _productService.GetSelectedSupplierNames());
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "ProductName,SupplierIdNames,CategoryIdNames,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")]
            UpdateProduct products)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(products);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _productService.GetSelectedCategoryNames());
            ViewData["SupplierId"] = new SelectList(await _productService.GetSelectedSupplierNames());

            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var products = await _productService.GetProductByIdAsync(id);
            if (products == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(await _productService.GetSelectedCategoryNames());
            ViewData["SupplierId"] = new SelectList(await _productService.GetSelectedSupplierNames());
            return View(Mapper.ToUpdateProductModel(products));
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "ProductName,SupplierIdNames,CategoryIdNames,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")]
            UpdateProduct products)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(id, products);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _productService.GetSelectedCategoryNames());
            ViewData["SupplierId"] = new SelectList(await _productService.GetSelectedSupplierNames());
            return View(products);
        }
    }
}