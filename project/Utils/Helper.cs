using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Interfaces;
using Models;

namespace Utils
{
    public class Helper : IHelper
    {
        private readonly ILogger<Helper> _logger;

        public Helper(ILogger<Helper> logger)
        {
            _logger = logger;
        }

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
                _logger.LogError($"There was a problem converting.");
            }

            return result;
        }

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
                 _logger.LogInformation($"You cannot add duplicate items.");
            }

            return result;
        }
    }
}
//idea
//uniqueDataEntry