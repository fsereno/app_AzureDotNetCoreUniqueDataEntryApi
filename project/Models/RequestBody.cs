using System.Collections.Generic;

namespace FabioSereno.App_AzureDotNetCoreUniqueDataEntryApi.Models
{
    public class RequestBody
    {
        public RequestBody()
        {
            this.Items = new List<Item>();
            this.Item = new Item();
        }
        public List<Item> Items { get; set; }

        public Item Item { get; set; }
    }
}