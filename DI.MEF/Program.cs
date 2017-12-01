using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace DI.MEF
{
    class Program
    {
        //MEF is different compared to other IoC containers..It depends on atrributes.check the classes
        public static CompositionContainer Container;

        static void Main(string[] args)
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            Container = new CompositionContainer(catalog);

            Console.WriteLine("MEF DI Container Example");
            Console.WriteLine();

            OrderInfo orderInfo = new OrderInfo()
            {
                CustomerName = "Shreesha",
                Email = "shreesha@gmail.com",
                Product = "Laptop",
                Price = 1200,
                CreditCard = "1234567890"
            };

            Commerce commerce = Container.GetExportedValue<Commerce>(); // requires commerce is MEF managed

            #region satisfy own imports

            //Commerce commerce = new Commerce(); // requires that Commerce satisfies its own imports
            //Container.SatisfyImportsOnce(commerce);

            #endregion

            commerce.ProcessOrder(orderInfo);

            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();


        }
    }
}
