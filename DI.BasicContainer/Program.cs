using System;

namespace DI.BasicContainer
{
    class Program
    {
        private static Resolver _resolver = new Resolver();
        static void Main(string[] args)
        {
            _resolver.Register<Commerce, Commerce>();
            _resolver.Register<IBillingProcessor, BillingProcessor>();
            _resolver.Register<ICustomer, Customer>();
            _resolver.Register<INotifier, Notifier>();
            _resolver.Register<ILogger, Logger>();

            Console.WriteLine("Basic DI Container Example");
            Console.WriteLine();

            OrderInfo orderInfo = new OrderInfo()
            {
                CustomerName = "Shreesha",
                Email = "shreesha@gmail.com",
                Product = "Laptop",
                Price = 1200,
                CreditCard = "1234567890"
            };

            Commerce commerce = _resolver.CreateType<Commerce>();
            commerce.ProcessOrder(orderInfo);

            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}
