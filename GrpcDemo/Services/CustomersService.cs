using Grpc.Core;

namespace GrpcService.Services
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
            
            if(request.UserId == 1)
            {
                output.FirstName = "Kanan";
                output.LastName = "Garazada";
            }
            else
            {
                output.FirstName = "John";
                output.LastName = "Doe";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Kanan",
                    LastName = "Garazada",
                    EmailAddress = "kanangarayev02@gmail.com",
                    Age= 20,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Bilbo",
                    LastName = "Baggins",
                    EmailAddress = "bilbo@gmail.com",
                    Age= 120,
                    IsAlive = false
                }
            };

            foreach (var customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
