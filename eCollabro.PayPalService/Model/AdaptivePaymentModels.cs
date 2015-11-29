using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersoft.PayPalService.Model
{
    public class PayPalPaymentDetailsRequest
    {
        public string PayKey { get; set; }
    }

    public class PayPalPayRequest
    {
        public string ReceiverEmail { get; set; }
        public decimal ReceiverAmount { get; set; }
        public string InvoiceId { get; set; }
        public string SenderEmail { get; set; }
    }
}
