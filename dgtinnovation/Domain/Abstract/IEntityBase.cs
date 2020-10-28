using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstract
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
