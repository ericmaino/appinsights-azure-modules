using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility
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
