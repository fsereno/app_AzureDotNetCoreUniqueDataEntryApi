using System.Collections.Generic;
using Models;
namespace Interfaces
{
    public interface IHelper
    {
        T Convert<T>(string data, T defautType);

        bool CanItemBeAdded(Dictionary<Item, string> dict, Item box);
    }
}