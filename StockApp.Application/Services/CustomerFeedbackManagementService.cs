using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    internal class CustomerFeedbackManagementService : ICustomerFeedbackManagementService
    {
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO)
        {
            var customerId =GenerateCustomerId();
            var customer = new CustomerDTO
            {
                Id = (int)customerId,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber,
            };
            return customer;
        }
        public string Feedback(string text)
        {
            if (text.Contains("bom") || text.Contains("otimo"))
            {
                return "Positive";
            }
            if (text.Contains("ruim") || text.Contains("terrivel"))
            {
                return "Negative";
            }

            return "Neutral";
        }

        private int GenerateCustomerId()
        {
            return 1; //para um id unico
        }

        public IEnumerable<CustomerDTO> AddCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
