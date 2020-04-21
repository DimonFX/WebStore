using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interface
{
    public interface IOrderedEntity
    {
        int Order { get; set; }
    }
}
