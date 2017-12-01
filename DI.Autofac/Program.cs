using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.AttributeMetadata;

namespace DI
{
    class Program
    {
        public static IContainer Container;

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Regular DI usage");
                Console.WriteLine("2 - Specific service locator");
                Console.WriteLine("3 - General service locator");
                Console.WriteLine("4 - Lifetime scope");
                Console.WriteLine("5 - Assembly scanning");
                Console.WriteLine("6 - Module usage");
                Console.WriteLine("7 - One-to-many");
                Console.WriteLine("0 - Exit");
                Console.WriteLine();
                Console.Write("Select demo initialization: ");
                string choice = Console.ReadLine();
                if (choice == "0")
                    exit = true;
                else
                {
                    OrderInfo orderInfo = new OrderInfo()
                    {
                        CustomerName = "Shreesha",
                        Email = "shreesha@gmail.com",
                        Product = "Laptop",
                        Price = 1200,
                        CreditCard = "1234567890"
                    };

                    Console.WriteLine();
                    Console.WriteLine("Order Processing:");
                    Console.WriteLine();

                    ContainerBuilder builder = new ContainerBuilder();

                    switch (choice)
                    {
                        case "1":
                            #region regular container usage
                            builder.RegisterType<Commerce1>();
                            builder.RegisterType<BillingProcessor>().As<IBillingProcessor>();
                            builder.RegisterType<CustomerProcessor>().As<ICustomerProcessor>();
                            builder.RegisterType<NotificationProcessor>().As<INotificationProcessor>();
                            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>();

                            Container = builder.Build();

                            Commerce1 commerce1 = Container.Resolve<Commerce1>();

                            commerce1.ProcessOrder(orderInfo);
                            #endregion

                            break;
                        case "2":
                            #region specific service locator (Commerce2)
                            builder.RegisterType<Commerce2>();
                            builder.RegisterType<BillingProcessor>().As<IBillingProcessor>();
                            builder.RegisterType<CustomerProcessor>().As<ICustomerProcessor>();
                            builder.RegisterType<NotificationProcessor>().As<INotificationProcessor>();
                            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>();
                            builder.RegisterType<BillingProcessorLocator>().As<IBillingProcessorLocator>();

                            Container = builder.Build();

                            Commerce2 commerce2 = Container.Resolve<Commerce2>();

                            commerce2.ProcessOrder(orderInfo);
                            #endregion

                            break;
                        case "3":
                            #region general service locator (Commerce3)
                            builder.RegisterType<Commerce3>();
                            builder.RegisterType<BillingProcessor>().As<IBillingProcessor>();
                            builder.RegisterType<CustomerProcessor>().As<ICustomerProcessor>();
                            builder.RegisterType<NotificationProcessor>().As<INotificationProcessor>();
                            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>();
                            builder.RegisterType<ProcessorLocator>().As<IProcessorLocator>();

                            Container = builder.Build();

                            Commerce3 commerce3 = Container.Resolve<Commerce3>();

                            commerce3.ProcessOrder(orderInfo);
                            #endregion

                            break;
                        case "4":
                            #region lifetime scope & singleton (Commerce4)
                            builder.RegisterType<Commerce4>();
                            builder.RegisterType<BillingProcessor>().As<IBillingProcessor>().InstancePerLifetimeScope();
                            builder.RegisterType<CustomerProcessor>().As<ICustomerProcessor>().InstancePerLifetimeScope();
                            builder.RegisterType<NotificationProcessor>().As<INotificationProcessor>().InstancePerLifetimeScope();
                            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>().InstancePerLifetimeScope();
                            builder.RegisterType<ProcessorLocator2>().As<IProcessorLocator2>();
                            builder.RegisterType<SingleTester>().As<ISingleTester>(); //.SingleInstance();

                            Container = builder.Build();

                            #region sample lifetime scope resolving
                            //using (ILifetimeScope scope = Container.BeginLifetimeScope())
                            //{
                            //    Commerce4 scopedCommerce = scope.Resolve<Commerce4>();
                            //}

                            // if dependencies were 'Disposable', they would now be disposed and released
                            // without lifetime scope, the container would hold on to disposable components
                            #endregion

                            Commerce4 commerce4 = Container.Resolve<Commerce4>();

                            commerce4.ProcessOrder(orderInfo);

                            Console.WriteLine("Press 'Enter' to process again...");
                            Console.ReadLine();

                            commerce4 = Container.Resolve<Commerce4>();
                            commerce4.ProcessOrder(orderInfo);

                            #endregion

                            break;
                        case "5":
                            #region assembly scanning (Commerce5)
                            builder.RegisterType<Commerce5>();
                            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                                .Where(t => t.Name.EndsWith("Processor"))
                                .As(t => t.GetInterfaces().FirstOrDefault(
                                    i => i.Name == "I" + t.Name));

                            Container = builder.Build();

                            Commerce5 commerce5 = Container.Resolve<Commerce5>();

                            commerce5.ProcessOrder(orderInfo);
                            #endregion

                            break;
                        case "6":
                            #region module usage (Commerce6)
                            builder.RegisterType<Commerce6>();
                            builder.RegisterModule<ProcessorRegistrationModule>();

                            Container = builder.Build();

                            Commerce6 commerce6 = Container.Resolve<Commerce6>();

                            commerce6.ProcessOrder(orderInfo);
                            #endregion

                            break;
                        case "7":
                            #region one-to-many (Commerce7)

                            builder.RegisterModule(new AttributedMetadataModule());
                            builder.RegisterType<Commerce7>().AsSelf();
                            builder.RegisterType<BillingProcessor>().As<IBillingProcessor>();
                            builder.RegisterType<CustomerProcessor>().As<ICustomerProcessor>();
                            builder.RegisterType<NotificationProcessor>().As<INotificationProcessor>();
                            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>();
                            builder.RegisterType<ProcessorLocator>().As<IProcessorLocator>();


                            // Regular Registration 
                            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                            //    .Where(t => t.Name.StartsWith("Plugin"))
                            //    .As<IPostOrderPlugin>();

                            // Regular Registration 
                            //builder.RegisterType<Plugin1>().As<IPostOrderPlugin>();
                            //builder.RegisterType<Plugin2>().As<IPostOrderPlugin>();
                            //builder.RegisterType<Plugin3>().As<IPostOrderPlugin>();


                            // Named Registration
                            builder.RegisterType<Plugin1>().Keyed<IPostOrderPlugin>("Plugin1");
                            builder.RegisterType<Plugin2>().Keyed<IPostOrderPlugin>("Plugin2");
                            builder.RegisterType<Plugin3>().Keyed<IPostOrderPlugin>("Plugin3");


                            // Specifying Which Class to use for Commerce7
                            var FinalImplentation = "Plugin2";
                            builder.Register(c => new Commerce7(c.Resolve<IProcessorLocator>(), c.ResolveNamed<IPostOrderPlugin>(FinalImplentation)));

                            Container = builder.Build();

                            Commerce7 commerce7 = Container.Resolve<Commerce7>();

                            commerce7.ProcessOrder(orderInfo);
                            #endregion
                            break;
                    }

                    Container.Dispose();
                    Console.WriteLine();
                    Console.WriteLine("Press 'Enter' for menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}
