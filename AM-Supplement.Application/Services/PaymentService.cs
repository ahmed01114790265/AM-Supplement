using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Supplement.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentResultDTO> PayAsync(PaymentRequestDTO request)
        {
            // 🔥 Mock Payment
            await Task.Delay(1500); // simulate payment gateway

            return new PaymentResultDTO
            {
                Success = true,
                TransactionId = Guid.NewGuid().ToString()
            };
        }
    }

}
