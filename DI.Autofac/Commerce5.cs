using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI
{
    public class Commerce5
    {
        public Commerce5(IBillingProcessor billingProcessor, ICustomerProcessor customer, INotificationProcessor notifier, ILoggingProcessor logger)
        {
            _billingProcessor = billingProcessor;
            _customer = customer;
            _notifier = notifier;
            _logger = logger;
        }

        private IBillingProcessor _billingProcessor;
        private ICustomerProcessor _customer;
        private INotificationProcessor _notifier;
        private ILoggingProcessor _logger;

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
