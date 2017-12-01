using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI
{
    public class Commerce2
    {
        public Commerce2(IBillingProcessorLocator billingProcessorLocator, ICustomerProcessor customer, INotificationProcessor notifier, ILoggingProcessor logger)
        {
            _billingProcessorLocator = billingProcessorLocator;
            _customer = customer;
            _notifier = notifier;
            _logger = logger;
        }

        private IBillingProcessorLocator _billingProcessorLocator;
        private ICustomerProcessor _customer;
        private INotificationProcessor _notifier;
        private ILoggingProcessor _logger;

        public void ProcessOrder(OrderInfo orderInfo)
        {
            IBillingProcessor billingProcessor = _billingProcessorLocator.GetBillingProcessor();

            billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            _logger.Log("Billing processed");
            _customer.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            _logger.Log("Customer updated");
            _notifier.SendReceipt(orderInfo);
            _logger.Log("Receipt sent");
        }
    }
}
