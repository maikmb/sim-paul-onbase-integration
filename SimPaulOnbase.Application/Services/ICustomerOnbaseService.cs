using SimPaulOnbase.Core.Customers;

namespace SimPaulOnbase.Application.Services
{
    public interface ICustomerOnbaseService
    {
        void Handle(Customer customer);
    }
}
