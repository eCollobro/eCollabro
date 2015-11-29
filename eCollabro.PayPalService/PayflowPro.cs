#region References
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
#endregion 

namespace Intersoft.PayPalService
{
    /// <summary>
    /// PayflowProRequest
    /// </summary>
    public class PayflowProRequest
    {
        #region Data Members 
        public string CreditCardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
        public double Amount { get; set; }
        public string InvoiceNumber { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public string Vendor { get; set; }
        public string Partner { get; set; }
        public string Password { get; set; }
        public string OrderId { get; set; }
        #endregion 
    }

    /// <summary>
    /// PayflowProRequest
    /// </summary>
    public class PayflowProRefundRequest
    {
        #region Data Members
        public double Amount { get; set; }
        public string User { get; set; }
        public string Vendor { get; set; }
        public string Partner { get; set; }
        public string Password { get; set; }
        public string TransactionId { get; set; }
        #endregion
    }

    /// <summary>
    /// PayflowProResponse
    /// </summary>
    public class PayflowProResponse
    {
        #region Data Members 
        public NameValueCollection RequestCollection { get; set; }
        public NameValueCollection ResponseCollection { get; set; }
        public string TransErrors { get; set; }
        public string Status { get; set; }
        #endregion 

    }

    /// <summary>
    /// PayflowPro
    /// </summary>
    public class PayflowPro:IPayflowPro
    {

        #region Methods 
        /// <summary>
        /// DoPayment
        /// </summary>
        /// <param name="paypalPayFlowRequest"></param>
        /// <returns></returns>
        public PayflowProResponse DoPayment(PayflowProRequest paypalPayFlowRequest)
        {

            try
            {
                PayflowProResponse payflowProResponse = new PayflowProResponse();

                string PayPalRequest = "TRXTYPE=S" //S - sale transaction
                 + "&TENDER=C" //C - Credit card
                 + "&ACCT=" + paypalPayFlowRequest.CreditCardNumber
                 + "&EXPDATE=" + Convert.ToString(paypalPayFlowRequest.ExpiryMonth) + Convert.ToString(paypalPayFlowRequest.ExpiryYear).Substring(2, 2)
                 + "&CVV2=" + paypalPayFlowRequest.CVV   //card validation value (card security code)               
                 + "&AMT=" + paypalPayFlowRequest.Amount
                 + "&INVNUM=" + paypalPayFlowRequest.InvoiceNumber
                 + "&COMMENT1=" + paypalPayFlowRequest.Comment
                 + "&USER=" + paypalPayFlowRequest.User
                 + "&VENDOR=" + paypalPayFlowRequest.Vendor
                 + "&PARTNER=" + paypalPayFlowRequest.Partner
                 + "&PWD=" + paypalPayFlowRequest.Password;


                // Create an instantce of PayflowNETAPI.
                PayflowNETAPI PayflowNETAPI = new PayflowNETAPI();

                // RequestId is a unique string that is required for each & every transaction. 
                // The merchant can use her/his own algorithm to generate this unique request id or 
                // use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
                string PayPalResponse = PayflowNETAPI.SubmitTransaction(PayPalRequest, PayflowUtility.RequestId);

                //place data from PayPal into a namevaluecollection
                payflowProResponse.RequestCollection = GetPayPalCollection(PayflowNETAPI.TransactionRequest);
                payflowProResponse.ResponseCollection = GetPayPalCollection(PayPalResponse);

                //show transaction errors if any
                payflowProResponse.TransErrors = PayflowNETAPI.TransactionContext.ToString();
                //show transaction status
                payflowProResponse.Status = PayflowUtility.GetStatus(PayPalResponse);

                return payflowProResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// DoRefund
        /// </summary>
        /// <param name="paypalPayFlowRequest"></param>
        /// <returns></returns>
        public PayflowProResponse DoRefund(PayflowProRefundRequest paypalPayFlowRefundRequest)
        {

            try
            {
                PayflowProResponse payflowProResponse = new PayflowProResponse();

                string PayPalRequest = "TRXTYPE=C" //C - credit transaction
                 + "&TENDER=P" //C - Credit card
                 + "&AMT=" + paypalPayFlowRefundRequest.Amount
                 + "&USER=" + paypalPayFlowRefundRequest.User
                 + "&VENDOR=" + paypalPayFlowRefundRequest.Vendor
                 + "&PARTNER=" + paypalPayFlowRefundRequest.Partner
                 + "&PWD=" + paypalPayFlowRefundRequest.Password
                 + "&ORIGID=" + paypalPayFlowRefundRequest.TransactionId;


                // Create an instantce of PayflowNETAPI.
                PayflowNETAPI PayflowNETAPI = new PayflowNETAPI();

                // RequestId is a unique string that is required for each & every transaction. 
                // The merchant can use her/his own algorithm to generate this unique request id or 
                // use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
                string PayPalResponse = PayflowNETAPI.SubmitTransaction(PayPalRequest, PayflowUtility.RequestId);

                //place data from PayPal into a namevaluecollection
                payflowProResponse.RequestCollection = GetPayPalCollection(PayflowNETAPI.TransactionRequest);
                payflowProResponse.ResponseCollection = GetPayPalCollection(PayPalResponse);

                //show transaction errors if any
                payflowProResponse.TransErrors = PayflowNETAPI.TransactionContext.ToString();
                //show transaction status
                payflowProResponse.Status = PayflowUtility.GetStatus(PayPalResponse);

                return payflowProResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// GetSeureToken
        /// </summary>
        /// <param name="payflowProRequest"></param>
        /// <returns></returns>
        public PayflowProResponse GetSeureToken(PayflowProRequest payflowProRequest)
        {
            try
            {
                string securityToken = Guid.NewGuid().ToString();

                PayflowProResponse paypalPayFlowResponse = new PayflowProResponse();

                string PayPalRequest = "TRXTYPE=A" //A - sale transaction
                         + "&AMT=" + payflowProRequest.Amount
                         + "&CURRENCY=USD"
                         + "&INVNUM=" + payflowProRequest.InvoiceNumber
                         + "&INVNUM=" + payflowProRequest.Comment
                         + "&CREATESECURETOKEN=Y"
                         + "&SECURETOKENID=" + securityToken
                         + "&USER=" + payflowProRequest.User
                         + "&VENDOR=" + payflowProRequest.Vendor
                         + "&PARTNER=" + payflowProRequest.Partner
                         + "&PWD=" + payflowProRequest.Password;

                // Create an instantce of PayflowNETAPI.
                PayflowNETAPI PayflowNETAPI = new PayflowNETAPI();

                // RequestId is a unique string that is required for each & every transaction. 
                // The merchant can use her/his own algorithm to generate this unique request id or 
                // use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
                string PayPalResponse = PayflowNETAPI.SubmitTransaction(PayPalRequest, PayflowUtility.RequestId);

                //place data from PayPal into a namevaluecollection
                paypalPayFlowResponse.RequestCollection = GetPayPalCollection(PayflowNETAPI.TransactionRequest);
                paypalPayFlowResponse.ResponseCollection = GetPayPalCollection(PayPalResponse);

                //show transaction errors if any
                paypalPayFlowResponse.TransErrors = PayflowNETAPI.TransactionContext.ToString();
                //show transaction status
                paypalPayFlowResponse.Status = PayflowUtility.GetStatus(PayPalResponse);

                return paypalPayFlowResponse;
            }
            catch (Exception)
            {
                throw; 
            }
        }

        /// <summary>
        /// GetPayPalCollection
        /// </summary>
        /// <param name="payPalInfo"></param>
        /// <returns></returns>
        private NameValueCollection GetPayPalCollection(string payPalInfo)
        {
            //place the responses into collection
            NameValueCollection PayPalCollection = new System.Collections.Specialized.NameValueCollection();
            string[] ArrayReponses = payPalInfo.Split('&');

            for (int i = 0; i < ArrayReponses.Length; i++)
            {
                string[] Temp = ArrayReponses[i].Split('=');
                PayPalCollection.Add(Temp[0], Temp[1]);
            }
            return PayPalCollection;
        }

        #endregion 
    }
}
