namespace DI.Coupled
{
    public class Commerce
    {
        public Commerce()
        {
            _billingProcessor = new BillingProcessor();
            _customer = new Customer();
            _notifier = new Notifier();
            _logger = new Logger();
        }

        private BillingProcessor _billingProcessor;
        private Customer _customer;
        private Notifier _notifier;
        private Logger _logger;

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
