#region References
using Intersoft.PaypalService.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion 

namespace Intersoft.PayPalService
{
    /// <summary>
    /// IExpressCheckout
    /// </summary>
    public interface IExpressCheckout
    {
        NameValueCollection SetExpressCheckout(SetExpressCheckoutRequest setExpressCheckoutRequest);

        NameValueCollection DoExpressCheckout(DoExpressCheckoutRequest doExpressCheckoutRequest);

        NameValueCollection GetExpressCheckoutDetails(GetExpressCheckoutDetailsRequest getExpressCheckoutDetailsRequest);

        NameValueCollection RefundTransaction(ExpressCheckoutRefundRequest expressCheckoutRefundRequest);

    }
}
