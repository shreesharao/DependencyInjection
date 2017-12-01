using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI
{
    public class Commerce3
    {
        public Commerce3(IProcessorLocator processorLocator)
        {
            _processorLocator = processorLocator;
        }

        private IProcessorLocator _processorLocator;

        public void ProcessOrder(OrderInfo orderInfo)
        {
            IBillingProcessor billingProcessor = _processorLocator.GetProcessor<IBillingProcessor>();
            ICustomerProcessor customerProcessor = _processorLocator.GetProcessor<ICustomerProcessor>();
            INotificationProcessor notificationProcessor = _processorLocator.GetProcessor<INotificationProcessor>();
            ILoggingProcessor loggingProcessor = _processorLocator.GetProcessor<ILoggingProcessor>();

            billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            loggingProcessor.Log("Billing processed");
            customerProcessor.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            loggingProcessor.Log("Customer updated");
            notificationProcessor.SendReceipt(orderInfo);
            loggingProcessor.Log("Receipt sent");
        }
    }
}
