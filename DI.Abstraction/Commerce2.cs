using DI.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.Abstraction
{
    // Interface Example
    public class Commerce2 : ICommerce2Injector
    {
        public Commerce2()
        {

        }

        public void InjectDependent(IBillingProcessor billingProcessor, 
            ICustomer customer, INotifier notifier, ILogger logger)
        {
            this._BillingProcessor = billingProcessor;
            this._Customer = customer;
            this._Notifier = notifier;
            this._Logger = logger;
        }

        IBillingProcessor _BillingProcessor;
        ICustomer _Customer;
        INotifier _Notifier;
        ILogger _Logger;

        public void ProcessOrder(OrderInfo orderInfo)
        {
            _BillingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            _Logger.Log("Interface Injection: Billing processed");
            _Customer.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            _Logger.Log("Interface Injection: Customer updated");
            _Notifier.SendReceipt(orderInfo);
            _Logger.Log("Interface Injection: Receipt sent");
        }
    }
}
