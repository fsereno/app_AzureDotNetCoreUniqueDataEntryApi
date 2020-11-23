using System.Collections.Generic;

namespace Models
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