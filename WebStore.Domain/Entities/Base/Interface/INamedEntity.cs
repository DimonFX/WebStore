using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.Entities.Base.Interface;

namespace WebStore.DomainNew.Entities
{
    public interface INamedEntity: IBaseEntity
    {
        string Name { get; set; }
    }
}
