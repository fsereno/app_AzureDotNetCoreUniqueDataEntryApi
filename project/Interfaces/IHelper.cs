using System.Collections.Generic;
using Models;
namespace Interfaces
{
    public interface IHelper
    {
        /// <summary>
        /// Converts string data to passed generic type. Falls back to a default type.
        /// </summary>
        /// <param name="data">Data as string</param>
        /// <param name="defautType">Default type passed by generic param</param>
        /// <typeparam name="T">The generic type to convert to</typeparam>
        /// <returns>Returns the generic type passed</returns>
        T Convert<T>(string data, T defautType);

        /// <summary>
        /// Testing to see if an item can be added, by attempting to add the passed item to the dictionary
        /// </summary>
        /// <param name="dictionary">A dictionary of exsiting collection</param>
        /// <param name="item">The item to be added</param>
        /// <returns>A bool, can the item be added or not ?</returns>
        bool CanItemBeAdded(Dictionary<Item, string> dictionary, Item item);
    }
}