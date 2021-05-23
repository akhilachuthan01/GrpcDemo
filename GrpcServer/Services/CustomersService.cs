using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Ajmal";
                output.LastName = "Edp";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Arjun";
                output.LastName = "Ashok";
            }
            else
            {
                output.FirstName = "Prithvi";
                output.LastName = "Raj";
            }

            return Task.FromResult(output);

        }

        public override async Task GetNewCustomers(
            NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Ajmal",
                    LastName = "Edp",
                    EmailAddress = "aj@edp.com",
                    Age = 28,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Athul",
                    LastName = "Babu",
                    EmailAddress = "athubabu@gmail.com",
                    Age = 29,
                    IsAlive = true
                },new CustomerModel
                {
                    FirstName = "Arjun",
                    LastName = "raj",
                    EmailAddress = "aj@er.com",
                    Age = 26,
                    IsAlive = true
                },new CustomerModel
                {
                    FirstName = "Sajith",
                    LastName = "S",
                    EmailAddress = "sajith@edp.com",
                    Age = 45,
                    IsAlive = true
                }
            };

            foreach (var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
