namespace Microsoft.IoT.ServiceFabric.Common.Serialization
{
    using Microsoft.ServiceFabric.Services.Remoting.V2;
    using System;
    using System.Collections.Generic;

    public class ServiceRemotingJsonSerializationProvider : IServiceRemotingMessageSerializationProvider
    {
        public IServiceRemotingMessageBodyFactory CreateMessageBodyFactory()
        {
            return new JsonMessageFactory();
        }

        public IServiceRemotingRequestMessageBodySerializer CreateRequestMessageSerializer(Type serviceInterfaceType, IEnumerable<Type> requestWrappedTypes, IEnumerable<Type> requestBodyTypes = null)
        {
            return new ServiceRemotingRequestJsonMessageBodySerializer(serviceInterfaceType, requestWrappedTypes);
        }

        public IServiceRemotingResponseMessageBodySerializer CreateResponseMessageSerializer(Type serviceInterfaceType, IEnumerable<Type> responseWrappedTypes, IEnumerable<Type> responseBodyTypes = null)
        {
            return new ServiceRemotingResponseJsonMessageBodySerializer(serviceInterfaceType, responseBodyTypes);
        }
    }
}
