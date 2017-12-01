using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.Abstraction
{
    // Setter Example
    public class Commerce1
    {
        public Commerce1()
        {

        }

        // Setup
        IBillingProcessor _billingProcessor;
        ICustomer _customer;
        INotifier _notifier;
        ILogger _logger;


        // Assign
        public IBillingProcessor BillingProcessor
        {
            set
            {
                this._billingProcessor = value;
            }
        }

        public ICustomer Customer
        {
            set
            {
                this._customer = value;
            }
        }

        public INotifier Notifier
        {
            set
            {
                this._notifier = value;
            }
        }

        public ILogger Logger
        {
            set
            {
                this._logger = value;
            }
        }

        public void ProcessOrder(OrderInfo orderInfo)
        {
            _billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            _logger.Log("Setter Injection: Billing processed");
            _customer.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            _logger.Log("Setter Injection: Customer updated");
            _notifier.SendReceipt(orderInfo);
            _logger.Log("Setter Injection: Receipt sent");
        }
    }
}
