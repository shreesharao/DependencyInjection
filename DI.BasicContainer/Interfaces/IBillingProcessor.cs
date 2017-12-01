namespace DI.BasicContainer
{
    public interface IBillingProcessor
    {
        void ProcessPayment(string customer, string creditCard, double price);
    }
}
