#region References 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion 

namespace Intersoft.PaypalService.Model
{
    /// <summary>
    /// SetExpressCheckoutRequest
    /// </summary>
    public class SetExpressCheckoutRequest
    {
        public double Amount { get; set; }
    }

    public class ExpressCheckoutRefundRequest
    {
        public double Amount { get; set; }
        public string TransactionId { get; set; }
    }

    /// <summary>
    /// DoExpressCheckoutRequest
    /// </summary>
    public class DoExpressCheckoutRequest
    {
        public double Amount { get; set; }

        public string Token { get; set; }

        public string PayerId { get; set; }

    }

    /// <summary>
    /// GetExpressCheckoutDetailsRequest
    /// </summary>
    public class GetExpressCheckoutDetailsRequest
    {
        public string Token { get; set; }
    }
}
