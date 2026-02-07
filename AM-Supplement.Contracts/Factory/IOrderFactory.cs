using AM_Supplement.Contracts.DTO;
using AMSupplement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.Factory
{
    public interface IOrderFactory
    {
        public CartDTO CreateCartDTO(Order order);
    }
}
