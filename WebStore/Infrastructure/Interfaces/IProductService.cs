using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter);

    }
}
