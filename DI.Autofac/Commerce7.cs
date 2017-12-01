using Autofac.Features.AttributeFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI
{
    public class Commerce7
    {

        // Named Instance
        public Commerce7(IProcessorLocator processorLocator, IPostOrderPlugin plugin)
        {
            _processorLocator = processorLocator;
            _plugin = plugin;
        }

        // All Plugins
        //public Commerce7(IProcessorLocator processorLocator,
        //    IEnumerable<IPostOrderPlugin> plugins)
        //{
        //    _ProcessorLocator = processorLocator;
        //    _Plugins = plugins;
        //}

        private IProcessorLocator _processorLocator;
        private IPostOrderPlugin _plugin;

        IEnumerable<IPostOrderPlugin> _Plugins;

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


            // Named Instance
            _plugin.DoSomething();

            // Regular Usage
            //foreach (IPostOrderPlugin plugin in _Plugins)
            //{
            //    plugin.DoSomething();
            //}
        }
    }
}
