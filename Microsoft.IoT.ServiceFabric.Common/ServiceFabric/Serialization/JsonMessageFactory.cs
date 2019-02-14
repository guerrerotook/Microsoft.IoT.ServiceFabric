namespace Microsoft.IoT.ServiceFabric.Common.Serialization
{
    using Microsoft.ServiceFabric.Services.Remoting.V2;

    public class JsonMessageFactory : IServiceRemotingMessageBodyFactory
    {
        

        public IServiceRemotingRequestMessageBody CreateRequest(string interfaceName, string methodName, int numberOfParameters, object wrappedRequestObject)
        {
            return new JsonRemotingRequestBody();
        }
        

        public IServiceRemotingResponseMessageBody CreateResponse(string interfaceName, string methodName, object wrappedResponseObject)
        {
            return new JsonRemotingResponseBody();
        }
    }
}
