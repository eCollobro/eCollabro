using eCollabro.Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace AMTdirect.ServicesContracts
{
    public static class AMTdirectRequest
    {
        [ThreadStatic]
        public static UserContextDC ActiveUser;
    }
    public class eCollabroMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        #region IDispatchMessageInspector

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //Retrieve Inbound Object from Request 
            //If needed can be implemented 
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //Retrieve Inbound Object from Request 
            var header = request.Headers.GetHeader<UserContextDC>("UserContext", "s");
            if (header != null)
            {
                OperationContext.Current.IncomingMessageProperties.Add("UserContext", header);
            }
            return null;
        }

        #endregion


        #region IClientMessageInspector

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //Instantiate new HeaderObject with values from ClientContext; 
            if (AMTdirectRequest.ActiveUser != null)
            {
                var typedHeader = new MessageHeader<UserContextDC>(AMTdirectRequest.ActiveUser);
                var untypedHeader = typedHeader.GetUntypedHeader("ActiveUser", "s");
                request.Headers.Add(untypedHeader);
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //Instantiate new HeaderObject with values from ClientContext; 
            //if needed can be implemented 
        }

        #endregion


        

        
    }
}

