using System;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;

namespace MeetEric.ApplicationInsights.Extensibility
{
    public class AzureConfigurationModule : ITelemetryModule
    {
        public AzureConfigurationModule()
        {
            ProductionKeyName = "ApplicationInsightsKey";
        }

        public string DeveloperKey { get; set; }

        public string ProductionKeyName { get; set; }

        public void Initialize(TelemetryConfiguration configuration)
        {
            var productionKey = String.Empty;

            if (!String.IsNullOrEmpty(ProductionKeyName))
            {
                productionKey = CloudConfigurationManager.GetSetting(ProductionKeyName);
            }

            if (String.IsNullOrEmpty(productionKey))
            {
                configuration.TelemetryChannel.DeveloperMode = true;
                productionKey = DeveloperKey;
            }

            configuration.InstrumentationKey = productionKey;
        }
    }
}
