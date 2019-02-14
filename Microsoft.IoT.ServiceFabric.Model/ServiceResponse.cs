namespace Microsoft.IoT.ServiceFabric.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ServiceResponse<Type>
    {
        public ServiceResponse(Type value)
        {
            Value = value;
        }

        [DataMember]
        public Type Value { get; internal set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public Exception Error { get; set; }

        [DataMember]
        public ResponseErrorType ErrorType { get; set; }

        [DataMember]
        public string Message { get; set; }

        public override string ToString()
        {
            string result = null;

            if (IsSuccess)
            {
                result = "Operation completed successfully";
            }
            else
            {
                result = $"Operation fail with ErrorType {ErrorType} | Message: {Message}";
            }

            return result;
        }
    }
}
