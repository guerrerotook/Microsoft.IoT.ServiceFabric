namespace Microsoft.IoT.ServiceFabric.Validation.Interfaces
{
    using Microsoft.IoT.ServiceFabric.Model;
    using Microsoft.IoT.ServiceFabric.Validation.Interfaces.Model;
    using Microsoft.ServiceFabric.Services.Remoting;
    using System.Threading.Tasks;

    public interface IValidationService : IService
    {
        Task<ServiceResponse<ValidationServiceResponse>> ValidateMessage(ServiceCoreRequest value);
    }
}
