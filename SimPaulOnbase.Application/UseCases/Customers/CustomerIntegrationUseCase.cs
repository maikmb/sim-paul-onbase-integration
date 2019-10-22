﻿using Microsoft.Extensions.Logging;
using SimPaulOnbase.Application.Boundaries.Customers;
using SimPaulOnbase.Application.Repositories;
using SimPaulOnbase.Application.Services;
using System;

namespace SimPaulOnbase.Application.UseCases.Customers
{
    /// <summary>
    /// CustomerIntegrationUseCase class
    /// </summary>
    public class CustomerIntegrationUseCase : ICustomerIntegrationUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOnbaseService _customerOnbaseService;
        private readonly ILogger<CustomerIntegrationUseCase> _logger;

        /// <summary>
        /// Constructor for CustomerIntegrationUseCase
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="customerOnbaseService"></param>
        public CustomerIntegrationUseCase(
            ICustomerRepository customerRepository, 
            ICustomerOnbaseService customerOnbaseService, 
            ILogger<CustomerIntegrationUseCase> logger)
        {
            _customerRepository = customerRepository;
            _customerOnbaseService = customerOnbaseService;
            _logger = logger;
        }


        /// <summary>
        /// Method Handle
        /// </summary>
        /// <returns>CustomerIntegrationOutput</returns>
        public CustomerIntegrationOutput Handle()
        {
            try
            {
                var divergedRegistrations = _customerRepository.DivergedRegistrations();                

                foreach (var customer in divergedRegistrations)
                {
                    _customerOnbaseService.Handle(customer);
                }

                return new CustomerIntegrationOutput(divergedRegistrations.Count);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error on executing Customer Integration Use Case. See exception for more details.");
                throw ex;
            }
        }
    }
}
