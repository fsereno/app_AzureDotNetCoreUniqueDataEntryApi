using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Interfaces;
using Models;

namespace Utils
{
    public class UniqueDataEntryUtil : IUniqueDataEntryUtil
    {
        private readonly ILogger<UniqueDataEntryUtil> _logger;

        public UniqueDataEntryUtil(ILogger<UniqueDataEntryUtil> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public T Convert<T>(string data, T defaultType)
        {
            var result = defaultType;

            if (String.IsNullOrEmpty(data)) {
                return result;
            }

            try
            {
                var requestBody = JsonConvert.DeserializeObject<T>(data);

                if (requestBody is T) {
                    result = requestBody;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("There was a problem converting.");
                _logger.LogError(exception.Message);
            }

            return result;
        }

        /// <inheritdoc/>
        public bool CanItemBeAdded(Dictionary<Item, string> dict, Item item)
        {
            var result = false;
            var currentCount = dict.Count;

            try
            {
                dict.Add(item, item.SecondName);
                result = dict.Count == currentCount + 1;
            }
            catch (Exception exception)
            {
                _logger.LogWarning("You cannot add duplicate items.");
                _logger.LogWarning(exception.Message);
            }

            return result;
        }
    }
}