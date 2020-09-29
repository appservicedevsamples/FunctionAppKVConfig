using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System.IO;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;

[assembly: FunctionsStartup(typeof(MyFakeNamespace.Startup))]
namespace MyFakeNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));

            FunctionsHostBuilderContext context = builder.GetContext();

            var builtConfig = builder.ConfigurationBuilder.Build();

            builder.ConfigurationBuilder
            .AddAzureKeyVault(builtConfig["AzureVaultURL"], keyVaultClient, new DefaultKeyVaultSecretManager())
            //.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
             // .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
           .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
        }
    }
}
