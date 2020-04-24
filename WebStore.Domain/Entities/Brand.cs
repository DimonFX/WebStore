using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.DomainNew.Entities.Base
{
    //[Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
