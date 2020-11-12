using System;
using System.Collections.Generic;

namespace Models
{
    public class Item
    {
        public Item()
        {
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.Contact = string.Empty;
            this.PostCode = string.Empty;
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Contact { get; set; }
        public string PostCode { get; set; }

        public class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public bool Equals(Item item1, Item item2)
            {
                if (item2 == null && item1 == null)
                {
                    return true;
                }
                else if (item1 == null || item2 == null)
                {
                    return false;
                }
                else if (item1.SecondName == item2.SecondName
                    && item1.Contact == item2.Contact
                    && item1.PostCode == item2.PostCode)
                {
                    return true;
                }
                else {
                    return false;
                }
            }

            public int GetHashCode(Item item)
            {
                var toHash = item.SecondName + item.Contact + item.PostCode;
                return toHash.GetHashCode();
            }
        }
    }
}