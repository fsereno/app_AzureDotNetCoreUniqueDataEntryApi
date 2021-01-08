using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FabioSereno.App_AzureDotNetCoreUniqueDataEntryApi.Interfaces;
using FabioSereno.App_AzureDotNetCoreUniqueDataEntryApi.Utils;

[assembly: FunctionsStartup(typeof(AzureFunctionDependencyInjection.Startup))]
namespace AzureFunctionDependencyInjection
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IUniqueDataEntryUtil, UniqueDataEntryUtil>();
        }
    }
}