using Intersoft.PayPalService.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersoft.PayPalService
{
    public interface IAdaptivePayment
    {
        /// <summary>
        /// Pay
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <param name="receiverAmount"></param>
        /// <param name="invoiceId"></param>
        /// <param name="senderEmail"></param>
        /// <returns></returns>
        NameValueCollection Pay(PayPalPayRequest payPalPayRequest);

        /// <summary>
        /// PaymentDetails
        /// </summary>
        /// <param name="PayKey"></param>
        /// <returns></returns>
        NameValueCollection PaymentDetails(PayPalPaymentDetailsRequest payPalPaymentDetailsRequest);
    }
}
