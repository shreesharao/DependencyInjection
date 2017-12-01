using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI.Abstraction.Interfaces
{
    public interface ICommerce2Injector
    {
        void InjectDependent(IBillingProcessor billingProcessor,
            ICustomer customer, INotifier notifier, ILogger logger);
    }
}
