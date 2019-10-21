using SimPaulOnbase.Application.Boundaries.Customers;

namespace SimPaulOnbase.Application.UseCases.Customers
{
    /// <summary>
    /// CustomerIntegrationUseCase interface
    /// </summary>
    public interface ICustomerIntegrationUseCase
    {
        CustomerIntegrationOutput Handle();
    }
}
