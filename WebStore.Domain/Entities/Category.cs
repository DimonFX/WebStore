using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.DomainNew.Entities.Base;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.DomainNew.Entities
{
    [Table("Categories")]
    public class Category: NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская секция (при наличии)
        /// </summary>
        public int? ParentId { get; set; }
        public int Order { get; set; }
        [ForeignKey("ParenId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
