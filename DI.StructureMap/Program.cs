using System;
using StructureMap;

namespace DI.StructureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container(); // requires full .NET 4 targetted framework
            container.Configure(reg => reg.For<IBillingProcessor>().Use<BillingProcessor>());
            container.Configure(reg => reg.For<ICustomer>().Use<Customer>());
            container.Configure(reg => reg.For<INotifier>().Use<Notifier>());
            container.Configure(reg => reg.For<ILogger>().Use<Logger>());

            Console.WriteLine("StructureMap DI Container Example");
            Console.WriteLine();

            OrderInfo orderInfo = new OrderInfo()
            {
                CustomerName = "Shreesha",
                Email = "shreesha@gmail.com",
                Product = "Laptop",
                Price = 1200,
                CreditCard = "1234567890"
            };

            Commerce commerce = container.GetInstance<Commerce>();
            commerce.ProcessOrder(orderInfo);

            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}
