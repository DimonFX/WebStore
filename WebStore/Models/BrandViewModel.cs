using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.Models
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// Отращает кол-во товаров на данный момент у этого брэнда
        /// </summary>
        public int CountProducts { get; set; }
    }
}
