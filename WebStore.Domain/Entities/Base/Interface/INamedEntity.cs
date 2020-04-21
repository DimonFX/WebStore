using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interface;

namespace WebStore.Domain.Entities
{
    public interface INamedEntity: IBaseEntity
    {
        string Name { get; set; }
    }
}
