using DI.Abstraction.Interfaces;
using System;

namespace DI.Abstraction
{
    class Program
    {
        private static OrderInfo _orderInfo = null;
        static void Main(string[] args)
        {
            Console.WriteLine("ABSTRACTION Example");
            Console.WriteLine();
            InitializeObject();
           
            bool exit = false;
            while (!exit)
            {
                int option = 0;
                Console.WriteLine("1.Contructor injection\n2.Setter injection\n3.Interface injection\n");
                Console.WriteLine("Enter the option:");
                if (!int.TryParse(Console.ReadLine(), result: out option))
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }
                if (option >= 4)
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }
                else
                {
                    switch (option)
                    {
                        case 1: ShowcaseConstructorInjection();
                            break;
                        case 2: ShowcaseSetterInjection();
                            break;
                        case 3: ShowcaseInterfaceInjection();
                            break;
                        case 0:
                        default: exit = true;
                            break;
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void InitializeObject()
        {
            _orderInfo = new OrderInfo()
            {
                CustomerName = "Shreesha",
                Email = "shreesha@gmail.com",
                Product = "Laptop",
                Price = 1200,
                CreditCard = "1234567890"
            };
        }

        private static void ShowcaseConstructorInjection()
        {
            #region Constructor Injection
            Commerce commerce = new Commerce(new BillingProcessor(),
                                             new Customer(),
                                             new Notifier(),
                                             new Logger());
            commerce.ProcessOrder(_orderInfo);
            #endregion

            Console.WriteLine();
            Console.WriteLine("===========================================================");
            Console.WriteLine();
        }

        private static void ShowcaseSetterInjection()
        {
            #region Setter Injection
            Commerce1 commerce1 = new Commerce1();

            //Passing dependency
            commerce1.BillingProcessor = new BillingProcessor();
            commerce1.Customer = new Customer();
            commerce1.Notifier = new Notifier();
            commerce1.Logger = new Logger();


            commerce1.ProcessOrder(_orderInfo);
            #endregion

            Console.WriteLine();
            Console.WriteLine("===========================================================");
            Console.WriteLine();
        }

        private static void ShowcaseInterfaceInjection()
        {
            #region Interface Injection
            Commerce2 commerce2 = new Commerce2();

            //Creating dependency
            IBillingProcessor billingProcessor = new BillingProcessor();
            ICustomer customer = new Customer();
            INotifier notifier = new Notifier();
            ILogger logger = new Logger();

            //Passing dependency
            commerce2.InjectDependent(billingProcessor, customer, notifier, logger);
            commerce2.ProcessOrder(_orderInfo);
            #endregion
        }
    }
}
