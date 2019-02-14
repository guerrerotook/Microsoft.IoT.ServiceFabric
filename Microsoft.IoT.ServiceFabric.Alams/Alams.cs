namespace Microsoft.IoT.ServiceFabric.Alams
{
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.IoT.ServiceFabric.Alams.Interfaces;
    using Microsoft.IoT.ServiceFabric.Alams.Interfaces.Model;
    using Microsoft.IoT.ServiceFabric.Common;
    using Microsoft.IoT.ServiceFabric.Model;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Alams : StatelessService, IAlarmService
    {
        public Alams(StatelessServiceContext context)
            : base(context)
        { }

        public async Task<ServiceResponse<AlarmServiceResponse>> ProcessMessage(ServiceCoreRequest value)
        {
            return new ServiceResponse<AlarmServiceResponse>(new AlarmServiceResponse()
            {
                HasAlarm = false
            })
            {
                IsSuccess = true
            };
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return this.CreateServiceRemotingInstanceListeners();
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            TelemetryConfiguration.Active.InstrumentationKey = Context.GetTelemetryClient().InstrumentationKey;
        }
    }
}
