using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.DomainNew.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
