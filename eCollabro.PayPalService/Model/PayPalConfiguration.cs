using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersoft.PayPalService.Model
{
    public class PayPalConfiguration
    {
        public string ApplicationId { get; set; }
        public string UserId { get; set; }
        public string Signature { get; set; }
        public string Pwd { get; set; }
        public string CancelURL { get; set; }
        public string ReturnURL { get; set; }

        // Creates a configuration map containing credentials and other required configuration parameters
        public Dictionary<string, string> GetConfigurationMap()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();

            configMap = GetConfig();

            // Signature Credential
            configMap.Add("account1.apiUsername", UserId);
            configMap.Add("account1.apiPassword", Pwd);
            configMap.Add("account1.apiSignature", Signature);
            configMap.Add("account1.applicationId", ApplicationId);

            // Sample Certificate Credential
            // configMap.Add("account2.apiUsername", "certuser_biz_api1.paypal.com");
            // configMap.Add("account2.apiPassword", "D6JNKKULHN3G5B8A");
            // configMap.Add("account2.apiCertificate", "resource/sdk-cert.p12");
            // configMap.Add("account2.privateKeyPassword", "password");
            // configMap.Add("account2.applicationId", "APP-80W284485P519543T");

            // Sandbox Email Address
            //configMap.Add("sandboxEmailAddress", "pp.devtools@gmail.com");

            return configMap;
        }

        // Creates a configuration map containing mode and other required configuration parameters
        public Dictionary<string, string> GetConfig()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();

            // Endpoints are varied depending on whether sandbox OR live is chosen for mode
            configMap.Add("mode", "sandbox");

            // These values are defaulted in SDK. If you want to override default values, uncomment it and add your value.
            // configMap.Add("connectionTimeout", "5000");
            // configMap.Add("requestRetries", "2");

            return configMap;
        }
    }
}
