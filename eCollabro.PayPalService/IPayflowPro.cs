#region References 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion 

namespace Intersoft.PayPalService
{
    /// <summary>
    /// IPayflowPro
    /// </summary>
    public interface IPayflowPro
    {
        PayflowProResponse DoPayment(PayflowProRequest paypalPayFlowRequest);

        PayflowProResponse GetSeureToken(PayflowProRequest payflowProRequest);

        PayflowProResponse DoRefund(PayflowProRefundRequest paypalPayFlowRefundRequest);
    }
}
