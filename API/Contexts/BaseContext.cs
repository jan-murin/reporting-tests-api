using System;
using System.Collections.Generic;
using System.Linq;

namespace Reporting.Tests.API.API.Contexts
{
    public class BaseContext
    {
        protected Dictionary<string, int> ContextDictionary { get; }

        public BaseContext()
        {
            ContextDictionary = new Dictionary<string, int>();
        }

        public int? GetIdBySubstring(string key)
        {
            if (!String.IsNullOrWhiteSpace(key))
            {
                return ContextDictionary.FirstOrDefault(d => d.Key.Contains(key)).Value;
            }

            return null;
        }

        public int GetIdOrException(string key)
        {
            if (!ContextDictionary.ContainsKey(key))
            {
                throw new Exception($"Key [{key}] was not found in dictionary: [{string.Join(",", new List<string>(ContextDictionary.Keys))}]");
            }

            return ContextDictionary[key];
        }
        
        public int GetIdOrDefault(string key)
        {
            key = key?.Trim();
            if (!string.IsNullOrEmpty(key))
            {
                if (!ContextDictionary.ContainsKey(key))
                {
                    // default for non empty value is non existent id
                    return 999999;
                }

                return ContextDictionary[key];   
            }
            
            // default for empty value is 0
            return default;
        }
        
        /// <summary>
        /// returns null when empty key
        /// returns 999999 when key not in dictionary
        /// returns ID when key in dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int? GetIdOrDefaultNullable(string key)
        {
            int idOrDefault = GetIdOrDefault(key);
            return idOrDefault == 0 ? (int?) null : idOrDefault;
        }
        
        public void AddItem(string key, int value, bool skipIfContains = false)
        {
            if (skipIfContains && ContextDictionary.ContainsKey(key))
            {
                return;
            }
            
            ContextDictionary.Add(key, value);
        }

        public void AddItems(Dictionary<string,int> items)
        {
            foreach (var valuePair in items.ToList())
            {
                AddItem(valuePair.Key, valuePair.Value);
            }
        }

        public void AddOrUpdate(string registryValue, int id)
        {
            var item = ContextDictionary.FirstOrDefault(kvp => kvp.Value == id);
            if (item.Key != null)
            {
                ContextDictionary.Remove(item.Key);    
            }
            
            AddItem(registryValue, id);
        }

        /// <summary>
        /// updates context item by its value, not key, value must exist
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateItem(string key, int value)
        {
            var item = ContextDictionary.Single(kvp => kvp.Value == value);
            ContextDictionary.Remove(item.Key);
            AddItem(key, value);
        }

        public bool ContainsKey(string key)
        {
            return ContextDictionary.ContainsKey(key);
        }

        public string GetKeyByValue(int value)
        {
            var item = ContextDictionary.Single(kvp => kvp.Value == value);
            return item.Key;
        }

        public Dictionary<string, int> GetItems()
        {
            return ContextDictionary;
        }
        
        public void RemoveItem(string key)
        {
            ContextDictionary.Remove(key);
        }
    }
}
