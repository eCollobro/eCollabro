#region References

using Intersoft.PaypalService.Model;
using Intersoft.PayPalService.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Intersoft.PayPalService
{
    /// <summary>
    /// ExpressCheckout
    /// </summary>
    public class ExpressCheckout : IExpressCheckout
    {
        #region Data Members

        PayPalConfiguration _payPalConfigurationtion = null;

        #endregion

        #region Constructor
        /// <summary>
        /// ExpressCheckout : Constructor 
        /// </summary>
        /// <param name="payPalConfiguration"></param>
        public ExpressCheckout(PayPalConfiguration payPalConfiguration)
        {
            _payPalConfigurationtion = payPalConfiguration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// SetExpressCheckout
        /// </summary>
        /// <returns></returns>
        public NameValueCollection SetExpressCheckout(SetExpressCheckoutRequest setExpressCheckoutRequest)
        {

            // Set the Method property of the request to POST.
            // Create POST data and convert it to a byte array.


            string postData = "USER=" + _payPalConfigurationtion.UserId;
            postData += "&PWD=" + _payPalConfigurationtion.Pwd;
            postData += "&SIGNATURE=" + _payPalConfigurationtion.Signature;
            postData += "&VERSION=" + "98.0";
            postData += "&PAYMENTREQUEST_0_PAYMENTACTION=" + "Sale";
            postData += "&PAYMENTREQUEST_0_AMT=" + setExpressCheckoutRequest.Amount;
            postData += "&NOSHIPPING=1";
            postData += "&PAYMENTREQUEST_0_CURRENCYCODE=USD";
            postData += "&RETURNURL=" + _payPalConfigurationtion.ReturnURL;
            postData += "&CANCELURL=" + _payPalConfigurationtion.CancelURL;
            postData += "&METHOD=" + "SetExpressCheckout";


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.

            return PostData(postData);
        }

        /// <summary>
        /// RefundTransaction
        /// </summary>
        /// <returns></returns>
        public NameValueCollection RefundTransaction(ExpressCheckoutRefundRequest expressCheckoutRefundRequest)
        {

            // Set the Method property of the request to POST.
            // Create POST data and convert it to a byte array.


             string postData = "USER=" + _payPalConfigurationtion.UserId;
            postData += "&PWD=" + _payPalConfigurationtion.Pwd;
            postData += "&SIGNATURE=" + _payPalConfigurationtion.Signature;
            postData += "&VERSION=" + "98.0";
            postData += "&METHOD=" + "RefundTransaction";
            postData += "&TRANSACTIONID=" + expressCheckoutRefundRequest.TransactionId;
            postData += "&AMT=" + expressCheckoutRefundRequest.Amount;
            postData += "&REFUNDTYPE=Partial";
 

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.

            return PostData(postData);
        }

        /// <summary>
        /// DoExpressCheckout
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="payerId"></param>
        /// <returns></returns>
        public NameValueCollection DoExpressCheckout(DoExpressCheckoutRequest doExpressCheckoutRequest)
        {

            // Set the Method property of the request to POST.
            // Create POST data and convert it to a byte array.

            string postData = "USER=" + _payPalConfigurationtion.UserId;
            postData += "&PWD=" + _payPalConfigurationtion.Pwd;
            postData += "&SIGNATURE=" + _payPalConfigurationtion.Signature;
            postData += "&VERSION=" + "98.0";
            postData += "&TOKEN=" + doExpressCheckoutRequest.Token;
            postData += "&PAYERID=" + doExpressCheckoutRequest.PayerId;
            postData += "&PAYMENTREQUEST_0_PAYMENTACTION=" + "Sale";
            postData += "&PAYMENTREQUEST_0_AMT=" + doExpressCheckoutRequest.Amount;
            postData += "&PAYMENTREQUEST_0_CURRENCYCODE=USD";
            postData += "&RETURNURL=" + _payPalConfigurationtion.ReturnURL;
            postData += "&CANCELURL=" + _payPalConfigurationtion.CancelURL;
            postData += "&METHOD=" + "DoExpressCheckoutPayment";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            return PostData(postData);

        }


        /// <summary>
        /// GetExpressCheckoutDetails
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public NameValueCollection GetExpressCheckoutDetails(GetExpressCheckoutDetailsRequest  getExpressCheckoutDetailsRequest)
        {

            // Set the Method property of the request to POST.
            // Create POST data and convert it to a byte array.

            string postData = "USER=" + ConfigurationManager.AppSettings["X-PAYPAL-SECURITY-USERID"];
            postData += "&PWD=" + ConfigurationManager.AppSettings["X-PAYPAL-SECURITY-PASSWORD"];
            postData += "&SIGNATURE=" + ConfigurationManager.AppSettings["X-PAYPAL-SECURITY-SIGNATURE"];
            postData += "&VERSION=" + "98.0";
            postData += "&TOKEN=" + getExpressCheckoutDetailsRequest.Token;
            postData += "&METHOD=" + "GetExpressCheckoutDetails";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            return PostData(postData);
        }

        /// <summary>
        /// PostData
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        private static NameValueCollection PostData(string postData)
        {
            WebRequest request = null;
            // Create a request using a URL that can receive a post. 
            if (Convert.ToString(ConfigurationManager.AppSettings["PaypalSandboxEnabled"]).Trim().ToUpper().Equals("TRUE"))
            {
                request = WebRequest.Create("https://api-3t.sandbox.paypal.com/nvp");
            }
            else
            {
                request = WebRequest.Create("https://api-3t.paypal.com/nvp");
            }

            
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postData.Length;
            // Get the request stream.
            StreamWriter dataStream = new StreamWriter(request.GetRequestStream());
            // Write the data to the request stream.
            dataStream.Write(postData);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.

            Stream dataStreamresp = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStreamresp);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            NameValueCollection qscoll = HttpUtility.ParseQueryString(responseFromServer);
            return qscoll;
        }

        #endregion 
    }
}
