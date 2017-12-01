using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI
{
    public class Commerce4
    {
        public Commerce4(IProcessorLocator2 processorLocator, ISingleTester singleTester)
        {
            _processorLocator = processorLocator;
            _singleTester = singleTester;
        }

        private IProcessorLocator2 _processorLocator;
        private ISingleTester _singleTester;

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

            _singleTester.DisplayCounter();

            _processorLocator.ReleaseScope();
        }
    }

    public interface ISingleTester
    {
        void DisplayCounter();
    }

    public class SingleTester : ISingleTester
    {
       private int _counter = 0;

        public void DisplayCounter()
        {
            _counter++;

            Console.WriteLine("Counter inside class 'SingleTester' is now: {0}", _counter);
        }
    }
}
