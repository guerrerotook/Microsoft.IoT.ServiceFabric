namespace Microsoft.IoT.ServiceFabric.Common.Serialization
{
    using Microsoft.ServiceFabric.Services.Remoting.V2;
    using System;

    internal class JsonRemotingResponseBody : IServiceRemotingResponseMessageBody
    {
        public object Value;

        public void Set(object response)
        {
            Value = response;
        }

        public object Get(Type paramType)
        {
            return Value;
        }
    }
}
