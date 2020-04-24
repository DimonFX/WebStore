using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.DomainNew.Entities.Base
{
    public class NamedEntity : INamedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
