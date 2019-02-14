namespace Microsoft.IoT.ServiceFabric.Common
{
    using Microsoft.ApplicationInsights;
    using System;
    using System.Fabric;

    public static class TelemetryClientFactory
    {
        private static string TelemetryConfigurationSectionName = "ApplicationInsights";
        private static string TelemetryConfigurationConfigurationName = "InstrumentationKey";

        public static TelemetryClient GetTelemetryClient(this ServiceContext value)
        {
            TelemetryClient client = new TelemetryClient();
            client.Context.Operation.Id = Guid.NewGuid().ToString();
            client.InstrumentationKey = value.ReadConfigurationSettingValue(TelemetryConfigurationSectionName, TelemetryConfigurationConfigurationName);
            return client;
        }
    }
}
