﻿using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Akhil" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);

            var clientRequest = new CustomerLookupModel { UserId = 1 };

            var customer = await customerClient.GetCustomerInfoAsync(clientRequest);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");


            Console.WriteLine("\n New Customer List \n");

            using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} :" +
                        $"{currentCustomer.EmailAddress}");
                }
            }

            Console.ReadLine();
        }
    }
}
