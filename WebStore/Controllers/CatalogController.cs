﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Shop(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductFilter { BrandId = brandId, CategoryId = categoryId });

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    Brand = p.Brand?.Name ?? string.Empty
                }).OrderBy(p => p.Order)
                    .ToList()
            };

            return View(model);
        }
        //public IActionResult ProductDetails()
        //{
        //    var curProduc = _productService.GetProducts(new ProductFilter()).FirstOrDefault();
        //    if (curProduc == null)
        //        return NotFound();
        //    var model = new ProductViewModel()
        //    {
        //        Id = curProduc.Id,
        //        Name = curProduc.Name,
        //        ImageUrl = curProduc.ImageUrl,
        //        Order = curProduc.Order,
        //        Price = curProduc.Price,
        //        BrandName = curProduc.Brand?.Name ?? string.Empty
        //    };

        //    return View(model);
        //}
        public IActionResult ProductDetails(int id)
        {

            var curProduc = _productService.GetProductById(id);

            if (curProduc == null)
                curProduc = _productService.GetProducts(new ProductFilter()).FirstOrDefault();//Заглушка из меню
            if (curProduc == null)
                return NotFound();
            var model = new ProductViewModel()
            {
                Id = curProduc.Id,
                Name = curProduc.Name,
                ImageUrl = curProduc.ImageUrl,
                Order = curProduc.Order,
                Price = curProduc.Price,
                Brand = curProduc.Brand?.Name ?? string.Empty
            };

            return View(model);
        }
    }
}