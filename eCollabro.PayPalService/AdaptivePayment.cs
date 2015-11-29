using Intersoft.PayPalService.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Intersoft.PayPalService
{
    public class AdaptivePayment:IAdaptivePayment
    {
        PayPalConfiguration _payPalConfigurationtion = null;

        public AdaptivePayment(PayPalConfiguration payPalConfiguration)
        {
            _payPalConfigurationtion = payPalConfiguration;
        }
    

        /// <summary>
        /// Handle Pay API calls
        /// </summary>
        /// <param name="contextHttp"></param>
        public NameValueCollection Pay(PayPalPayRequest payPalPayRequest)
        {
                                      
            ReceiverList receiverList = new ReceiverList();
            receiverList.receiver = new List<Receiver>();



            Receiver rec = new Receiver(payPalPayRequest.ReceiverAmount);
            if (!string.IsNullOrEmpty(payPalPayRequest.ReceiverEmail))
                rec.email = payPalPayRequest.ReceiverEmail;
            if (string.IsNullOrEmpty(payPalPayRequest.InvoiceId))
                    rec.invoiceId =payPalPayRequest.InvoiceId;
                receiverList.receiver.Add(rec);
    
            PayRequest request = new PayRequest(new RequestEnvelope("en_US"),"PAY",
                               _payPalConfigurationtion.CancelURL,"USD",
                                receiverList, _payPalConfigurationtion.ReturnURL);

            // Sender's email address 
            if (string.IsNullOrEmpty(payPalPayRequest.SenderEmail))
                request.senderEmail = payPalPayRequest.SenderEmail;

            AdaptivePaymentsService service = null;
            PayResponse response = null;
            try
            {
                // Creating service wrapper object to make an API call and loading
                // configuration map for your credentials and endpoint
                service = new AdaptivePaymentsService(_payPalConfigurationtion.GetConfigurationMap());
                response = service.Pay(request);
            }
            catch (Exception)
            {
                throw; 
            }

            NameValueCollection responseValues = new NameValueCollection();
            if (!(response.responseEnvelope.ack == AckCode.FAILURE) &&
                !(response.responseEnvelope.ack == AckCode.FAILUREWITHWARNING))
            {
               
                responseValues.Add("PayKey", response.payKey);
                // The status of the payment. Possible values are:
                // CREATED – The payment request was received; funds will be transferred once the payment is approved
                // COMPLETED – The payment was successful
                // INCOMPLETE – Some transfers succeeded and some failed for a parallel payment or, for a delayed chained payment, secondary receivers have not been paid
                // ERROR – The payment failed and all attempted transfers failed or all completed transfers were successfully reversed
                // REVERSALERROR – One or more transfers failed when attempting to reverse a payment
                // PROCESSING – The payment is in progress
                // PENDING – The payment is awaiting processing
                responseValues.Add("PaymentExecutionStatus", response.paymentExecStatus);
                if (response.defaultFundingPlan != null && response.defaultFundingPlan.senderFees != null)
                {
                    //Fees to be paid by the sender.
                    responseValues.Add("SenderFees", response.defaultFundingPlan.senderFees.amount +
                                                response.defaultFundingPlan.senderFees.code);
                }


                responseValues.Add("Acknowledgement", response.responseEnvelope.ack.ToString());
            }
            return responseValues;
        }


        /// <summary>
        /// Handle PaymentDetails API call
        /// </summary>
        /// <param name="contextHttp"></param>
        public NameValueCollection PaymentDetails(PayPalPaymentDetailsRequest payPalPaymentDetailsRequest)
        {
             PaymentDetailsRequest request = new PaymentDetailsRequest(new RequestEnvelope("en_US"));
            // set optional parameters
            //(Optional) The pay key that identifies the payment for which 
            // you want to retrieve details. This is the pay key returned in the PayResponse message. 
             request.payKey = payPalPaymentDetailsRequest.PayKey;

           
            AdaptivePaymentsService service = null;
            PaymentDetailsResponse response = null;
            try
            {
                Dictionary<string, string> configurationMap = _payPalConfigurationtion.GetConfigurationMap();

                // Creating service wrapper object to make an API call and loading
                // configuration map for your credentials and endpoint
                service = new AdaptivePaymentsService(configurationMap);
                response = service.PaymentDetails(request);
            }
            catch (System.Exception ex)
            {
                throw;
            }

            NameValueCollection responseValues = new NameValueCollection();
            if (!(response.responseEnvelope.ack == AckCode.FAILURE) &&
                !(response.responseEnvelope.ack == AckCode.FAILUREWITHWARNING))
            {
                //(Optional) The pay key that identifies the payment 
                responseValues.Add("Paykey", response.payKey);
                // The status of the payment. Possible values are:
                // CREATED – The payment request was received; funds will be transferred once the payment is approved
                // COMPLETED – The payment was successful
                // INCOMPLETE – Some transfers succeeded and some failed for a parallel payment or, for a delayed chained payment, secondary receivers have not been paid
                // ERROR – The payment failed and all attempted transfers failed or all completed transfers were successfully reversed
                // REVERSALERROR – One or more transfers failed when attempting to reverse a payment
                // PROCESSING – The payment is in progress
                // PENDING – The payment is awaiting processing
                responseValues.Add("PaymentExecutionStatus", response.status);

                // The sender's email address. 
                responseValues.Add("SenderEmail", response.senderEmail);

                //Acknowledgement code. It is one of the following values:
                // Success – The operation completed successfully.
                // Failure – The operation failed.
                // SuccessWithWarning – The operation completed successfully; however, there is a warning message.
                // FailureWithWarning – The operation failed with a warning message.
                responseValues.Add("Acknowledgement", response.responseEnvelope.ack.ToString());

                // Whether the Pay request is set up to create a payment request with the SetPaymentOptions 
                // request, and then fulfill the payment with the ExecutePayment request. 
                // Possible values are:
                // PAY – Use this option if you are not using the Pay request in combination with ExecutePayment.
                // CREATE – Use this option to set up the payment instructions with SetPaymentOptions and then execute the payment at a later time with the ExecutePayment.
                // PAY_PRIMARY – For chained payments only, specify this value to delay payments to the secondary receivers; only the payment to the primary receiver is processed.
                responseValues.Add("ActionType", response.actionType);
            }
            return responseValues;
        }
        ///////// <summary>
        ///////// Utility method for displaying API response
        ///////// </summary>
        ///////// <param name="contextHttp"></param>
        ///////// <param name="apiName"></param>
        ///////// <param name="responseValues"></param>
        ///////// <param name="requestPayload"></param>
        ///////// <param name="responsePayload"></param>
        ///////// <param name="errorMessages"></param>
        ///////// <param name="redirectUrl"></param>
        //////private void Display(HttpContext contextHttp, string apiName, Dictionary<string, string> responseValues, string requestPayload, string responsePayload, List<ErrorData> errorMessages, string redirectUrl)
        //////{

        //////    contextHttp.Response.Write("<html><head><title>");
        //////    contextHttp.Response.Write("PayPal Adaptive Payments - " + apiName);
        //////    contextHttp.Response.Write("</title><link rel='stylesheet' href='Content/sdk.css' type='text/css'/></head><body>");
        //////    contextHttp.Response.Write("<h3>" + apiName + " response</h3>");
        //////    if (errorMessages != null && errorMessages.Count > 0)
        //////    {
        //////        contextHttp.Response.Write("<div class='section_header'>Error messages</div>");
        //////        contextHttp.Response.Write("<div class='note'>Investigate the response object for further error information</div><ul>");
        //////        foreach (ErrorData error in errorMessages)
        //////        {
        //////            contextHttp.Response.Write("<li>" + error.message + "</li>");
        //////        }
        //////        contextHttp.Response.Write("</ul>");
        //////    }
        //////    if (redirectUrl != null)
        //////    {
        //////        string red = "<div>This API involves a web flow. You must now redirect your user to " + redirectUrl;
        //////        red = red + "<br />Please click <a href='" + redirectUrl + "' target='_parent'>here</a> to try the flow.</div><br/>";
        //////        contextHttp.Response.Write(red);
        //////    }
        //////    contextHttp.Response.Write("<div class='section_header'>Key values from response</div>");
        //////    contextHttp.Response.Write("<div class='note'>Consult response object and reference doc for complete list of response values.</div><table>");

        //////    foreach (KeyValuePair<string, string> entry in responseValues)
        //////    {
        //////        contextHttp.Response.Write("<tr><td class='label'>");
        //////        contextHttp.Response.Write(entry.Key);
        //////        contextHttp.Response.Write(": </td><td>");

        //////        if (entry.Key == "Redirect To PayPal")
        //////        {
        //////            contextHttp.Response.Write("<a id='");
        //////            contextHttp.Response.Write(entry.Key);
        //////            contextHttp.Response.Write("' href=");
        //////            contextHttp.Response.Write(entry.Value);
        //////            contextHttp.Response.Write(">Redirect To PayPal</a>");
        //////        }
        //////        else
        //////        {
        //////            contextHttp.Response.Write("<div id='");
        //////            contextHttp.Response.Write(entry.Key);
        //////            contextHttp.Response.Write("'>");
        //////            contextHttp.Response.Write(entry.Value);
        //////        }

        //////        contextHttp.Response.Write("</td></tr>");
        //////    }

        //////    contextHttp.Response.Write("</table><h4>Request:</h4><br/><textarea rows=15 cols=80 readonly>");
        //////    contextHttp.Response.Write(requestPayload);
        //////    contextHttp.Response.Write("</textarea><br/><h4>Response</h4><br/><textarea rows=15 cols=80 readonly>");
        //////    contextHttp.Response.Write(responsePayload);
        //////    contextHttp.Response.Write("</textarea>");
        //////    contextHttp.Response.Write("<br/><br/><a href='Default.aspx'>Home<a><br/><br/></body></html>");
        //////}
    }
}
