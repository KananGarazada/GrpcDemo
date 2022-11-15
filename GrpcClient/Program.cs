using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemo;
using GrpcService;

//var input = new HelloRequest { Name= "Kanan" }; 
//var channel = GrpcChannel.ForAddress("https://localhost:7125");
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);

//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("https://localhost:7125");
var customerClient = new Customer.CustomerClient(channel);

var clientRequested = new CustomerLookupModel { UserId = 1 };

var customer = await customerClient.GetCustomerInfoAsync(clientRequested);

Console.WriteLine($"{customer.FirstName} {customer.LastName}");
Console.WriteLine("==============================New Customer List================================");
using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
	while (await call.ResponseStream.MoveNext())
	{
		var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
    }
}

Console.ReadLine();