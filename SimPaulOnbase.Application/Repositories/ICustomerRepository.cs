using SimPaulOnbase.Core.Customers;
using System.Collections.Generic;

namespace SimPaulOnbase.Application.Repositories
{
    /// <summary>
    /// ICustomerRepository interface
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get Customers Diverged Registrions
        /// </summary>
        /// <returns></returns>
        IList<Customer> DivergedRegistrations();
    }
}
