using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Entities.Base;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductService : IProductService
    {
        List<Category> _categories;
        List<Brand> _brands;
        List<Product> _products;

        public InMemoryProductService()
        {
            
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categories;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _brands;
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var products = _products;

            if (filter.CategoryId.HasValue)
            {
                products = products
                    .Where(x => x.CategoryId == filter.CategoryId.Value)
                    .ToList();
            }
            if (filter.BrandId.HasValue)
            {
                products = products
                    .Where(p => p.BrandId.HasValue && p.BrandId.Value == filter.BrandId.Value)
                    .ToList();
            }

            return products;
        }
    }
}
