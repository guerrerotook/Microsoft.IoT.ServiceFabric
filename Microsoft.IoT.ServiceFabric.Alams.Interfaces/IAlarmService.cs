namespace Microsoft.IoT.ServiceFabric.Alams.Interfaces
{
    using Microsoft.IoT.ServiceFabric.Alams.Interfaces.Model;
    using Microsoft.IoT.ServiceFabric.Model;
    using Microsoft.ServiceFabric.Services.Remoting;
    using System.Threading.Tasks;

    public interface IAlarmService: IService
    {
        Task<ServiceResponse<AlarmServiceResponse>> ProcessMessage(ServiceCoreRequest value);
    }
}
