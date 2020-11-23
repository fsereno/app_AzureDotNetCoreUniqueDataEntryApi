// Startup.cs
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Interfaces;
using Utils;

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