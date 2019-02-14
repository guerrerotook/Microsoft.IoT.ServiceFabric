namespace Microsoft.IoT.ServiceFabric.Common
{
    using System;
    using System.Fabric;

    public static class ServiceFabricConfigurationManager
    {
        public static string ReadConfigurationSettingValue(this ServiceContext value, string configurationSection, string configurationValue)
        {
            string result = null;

            value.EnsureIsNotNull(nameof(value), "ServiceContext is null");
            configurationSection.EnsureIsNotNullOrEmpty(nameof(configurationSection), "ConfigurationSection value is null or empty");
            configurationValue.EnsureIsNotNullOrEmpty(nameof(configurationValue), "ConfigurationValue value is null or empty");

            var configurationPackage = value.CodePackageActivationContext.GetConfigurationPackageObject("Config");
            var section = configurationPackage.Settings.Sections[configurationSection];
            if (section != null)
            {
                var settingValue = section.Parameters[configurationValue];
                result = settingValue.Value;
            }
            else
            {
                throw new InvalidOperationException();
            }


            return result;
        }
    }
}
