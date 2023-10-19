using CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Interfaces
{
    public interface IOrderService
    {
       Task<bool> AddOrder(AddOrder form);
    }
}
