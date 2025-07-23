using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(float totalPrice, int orderId, string description, string clientIp);
        bool VerifyHash(Dictionary<string, string> input);
    }
}
