using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Interfaces;
using Models;

namespace Azure.Function
{
    public class UniqueDataEntryHttpTriggerCSharp
    {
        private IUniqueDataEntryUtil _uniqueDataEntryUtil;

        public UniqueDataEntryHttpTriggerCSharp(IUniqueDataEntryUtil uniqueDataEntryUtil)
        {
            _uniqueDataEntryUtil = uniqueDataEntryUtil;
        }

        [FunctionName("CanItemBeAddedAsync")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            RequestBody data = null;
            var equalityComparer = new Item.ItemEqualityComparer();

            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                var requestBody = await streamReader.ReadToEndAsync();
                var defaultType = new RequestBody();
                log.LogInformation("requestBody: " + requestBody);
                data = _uniqueDataEntryUtil.Convert<RequestBody>(requestBody, defaultType);
            }

            var dictionary = data.Items.ToDictionary(x => x, x => x.FirstName, equalityComparer);
            var result = _uniqueDataEntryUtil.CanItemBeAdded(dictionary, data.Item);

            log.LogInformation($"Result is: {result}");

            return new OkObjectResult(result);
        }
    }
}