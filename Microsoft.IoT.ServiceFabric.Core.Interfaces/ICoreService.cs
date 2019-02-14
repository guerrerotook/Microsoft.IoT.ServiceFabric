namespace Microsoft.IoT.ServiceFabric.Core.Interfaces
{
    using Microsoft.IoT.ServiceFabric.Core.Interfaces.Model;
    using Microsoft.IoT.ServiceFabric.Model;
    using Microsoft.ServiceFabric.Services.Remoting;
    using System.Threading.Tasks;

    public interface ICoreService : IService
    {
        Task<ServiceResponse<CoreServiceResponse>> ProcessMessage(ServiceCoreRequest value);
    }
}
