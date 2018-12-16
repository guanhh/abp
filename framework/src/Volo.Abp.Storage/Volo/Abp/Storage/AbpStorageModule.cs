using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Volo.Abp.Modularity;
using Volo.Abp.Storage.Configuration;

namespace Volo.Abp.Storage
{
    public class AbpStorageModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configurationRoot = context.Services.GetConfiguration();

            Configure<AbpStorageOptions>(
                    configurationRoot.GetSection(AbpStorageOptions.DefaultConfigurationSectionName));

            Configure<AbpStorageOptions>(storageOptions =>
            {
                var connectionStrings = new Dictionary<string, string>();
                configurationRoot.GetSection("ConnectionStrings").Bind(connectionStrings);

                if (storageOptions.ConnectionStrings != null)
                    foreach (var existingConnectionString in storageOptions.ConnectionStrings)
                        connectionStrings[existingConnectionString.Key] = existingConnectionString.Value;

                storageOptions.ConnectionStrings = connectionStrings;
            });
        }
    }
}