using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI.Unity
{
    public class Commerce
    {
        public Commerce(IBillingProcessor billingProcessor, ICustomer customer, INotifier notifier, ILogger logger)
        {
            _billingProcessor = billingProcessor;
            _customer = customer;
            _notifier = notifier;
            _logger = logger;
        }

        private IBillingProcessor _billingProcessor;
        private ICustomer _customer;
        private INotifier _notifier;
        private ILogger _logger;

        public void ProcessOrder(OrderInfo orderInfo)
        {
            _billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            _logger.Log("Billing processed");
            _customer.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            _logger.Log("Customer updated");
            _notifier.SendReceipt(orderInfo);
            _logger.Log("Receipt sent");
        }
    }
}
