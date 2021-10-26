using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JonasDemoApi.MessageHandlers
{
    public class ApiKeyMessageHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> requestHeaders;

            string ApiKey = GetApiKey();

            var checkApiKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
            if(checkApiKeyExists)
            {
                if (requestHeaders.FirstOrDefault().Equals(ApiKey))
                {
                    validKey = true;
                }
            }
           
            if(!validKey)
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key!");
    

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);

            return response;

        }

        private string GetApiKey()
        {
            var apiKey = ConfigurationManager.AppSettings["APIKey"];

            if (apiKey == null)
                throw new ConfigurationErrorsException("No API Key was found");

            return apiKey;
        } 
    }

}