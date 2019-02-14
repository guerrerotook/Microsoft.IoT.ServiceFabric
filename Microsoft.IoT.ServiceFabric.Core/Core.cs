namespace Microsoft.IoT.ServiceFabric.Core
{
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.IoT.ServiceFabric.Alams.Interfaces;
    using Microsoft.IoT.ServiceFabric.Alams.Interfaces.Model;
    using Microsoft.IoT.ServiceFabric.Common;
    using Microsoft.IoT.ServiceFabric.Common.Serialization;
    using Microsoft.IoT.ServiceFabric.Core.Interfaces;
    using Microsoft.IoT.ServiceFabric.Core.Interfaces.Model;
    using Microsoft.IoT.ServiceFabric.Model;
    using Microsoft.ServiceFabric.Services.Client;
    using Microsoft.ServiceFabric.Services.Communication.Client;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class Core : StatefulService, ICoreService
    {
        private ServiceProxyFactory proxyFactory;
        private TelemetryClient telemetry;

        public Core(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<ServiceResponse<CoreServiceResponse>> ProcessMessage(ServiceCoreRequest value)
        {
            ServiceResponse<CoreServiceResponse> result = new ServiceResponse<CoreServiceResponse>(new CoreServiceResponse());
            if (value != null)
            {
                // do the logic
                result.IsSuccess = true;

                IAlarmService proxy = proxyFactory.CreateServiceProxy<IAlarmService>(
                    new Uri(
                        string.Concat(Context.CodePackageActivationContext.ApplicationName, "/Microsoft.IoT.ServiceFabric.Alams")),
                    new ServicePartitionKey(0L),
                    TargetReplicaSelector.PrimaryReplica);

                using (MeasureTime measure = new MeasureTime())
                {

                    ServiceResponse<AlarmServiceResponse> response = await proxy.ProcessMessage(value);

                    response.IsSuccess = response != null;

                    telemetry.TrackEvent(
                        "Core-Alarm-Call", 
                        new Dictionary<string, string>() { { "Id", value.Id } }, 
                        new Dictionary<string, double>() { { "CoreElapsed", measure.Elapsed.TotalMilliseconds } });
                }

                telemetry.TrackEvent(
                       "CoreService",
                       new Dictionary<string, string>() { { "Id", value.Id } });
            }
            else
            {
                result.IsSuccess = false;
                result.Error = new ArgumentNullException(nameof(value));
            }

            return result;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                 new ServiceReplicaListener((c) =>
                 {
                     return new FabricTransportServiceRemotingListener(
                         c,
                         this,
                         serializationProvider: new ServiceRemotingJsonSerializationProvider());

                 })
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            telemetry = Context.GetTelemetryClient();
            TelemetryConfiguration.Active.InstrumentationKey = telemetry.InstrumentationKey;

            proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory(serializationProvider: new ServiceRemotingJsonSerializationProvider());
            });
        }
    }
}
