using Autofac;

namespace DI
{
    public class BillingProcessorLocator : IBillingProcessorLocator
    {
        IBillingProcessor IBillingProcessorLocator.GetBillingProcessor()
        {
            return Program.Container.Resolve<IBillingProcessor>();
        }
    }
}