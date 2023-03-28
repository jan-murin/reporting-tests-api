using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Reporting.Tests.API.API.Infrastructure
{
    public static class Customers
    {
        private static readonly IDictionary<string, CustomerInfo> CustomersDictionary = new Dictionary<string, CustomerInfo>();

        [ThreadStatic] public static CustomerInfo DefaultCustomer;
        [ThreadStatic] public static CustomerInfo CurrentCustomer;

        public static void RegisterCustomer(CustomerInfo customer)
        {
            var key = $"{customer.CustomerName}";

            if (CustomersDictionary.ContainsKey(key))
            {
                CurrentCustomer = CustomersDictionary[key];
                return;
            }

            CustomersDictionary.Add(key, customer);
            CurrentCustomer = customer;
        }


        public static CustomerInfo GetCustomer(string customerName)
        {
            var key = $"{customerName}_{TestContext.CurrentContext.Test.Name}";
            if (CustomersDictionary == null || !CustomersDictionary.ContainsKey(key))
            {
                throw new Exception($"User {key} not found in the context");
            }

            return CustomersDictionary[key];
        }
    }
}
