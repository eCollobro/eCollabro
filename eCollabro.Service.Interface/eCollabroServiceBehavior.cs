using AMTdirect.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace AMTdirect.ServicesContracts
{
    public class eCollabroServiceBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint,
        BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint,
        ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new eCollabroMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
        EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(
        ServiceEndpoint endpoint)
        {
        }

        #endregion

        public override Type BehaviorType
        {
            get
            {
                return typeof(eCollabroServiceBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new eCollabroServiceBehavior();
        }
    }

    public class AMTdirectInspectorBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new eCollabroServiceBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(eCollabroServiceBehavior); }
        }
    }

}
