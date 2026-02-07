using AM_Supplement.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Contracts.Services
{
    public interface IPaymentService
    {
        Task<PaymentResultDTO> PayAsync(PaymentRequestDTO request);
    }

}
