using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public BrandsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = _productService.GetBrands();
            var query = _productService.GetProducts(new DomainNew.ProductFilter())
                .GroupBy(x => x.BrandId);
            // получим и заполним родительские категории
            //var parentSections = brands.Where(p => !p.ParentId.HasValue).ToArray();
            var result = new List<BrandViewModel>();
            foreach (var curProduct in query)
            {
                var curBrand = brands.FirstOrDefault(x => x.Id == curProduct.Key);
                if (curBrand == null) continue;
                result.Add(new BrandViewModel()
                {
                    Id = curBrand.Id,
                    Name = curBrand.Name,
                    Order = curBrand.Order,
                    CountProducts = curProduct.Count()
                });
            }
            return result;
        }
    }
}
