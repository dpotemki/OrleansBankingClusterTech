

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrainInterfaces;


Console.WriteLine("Hello, World!");
var dbConnectionString = "Server=localhost;Database=OrleansStorage;User=sa;Password=Akjd3_as77;";
var invariant = "System.Data.SqlClient";

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseAdoNetClustering(options =>
        {
            options.Invariant = invariant;
            options.ConnectionString = dbConnectionString;
        });
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

using IHost host = builder.Build();
await host.StartAsync();




IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

for(var i = 0; i < 10_000_000; i++)
{
    IHello friend = client.GetGrain<IHello>(i);
    string response = await friend.SayHello($"Hi friend {i}!");
    Console.WriteLine(response);
}

int userId = 10;
IAccountGrain accountGrain = client.GetGrain<IAccountGrain>(userId);
await accountGrain.CreateAccount();
var balance = await accountGrain.GetBalance();
await accountGrain.Deposit(100);
await accountGrain.Withdraw(500);
balance = await accountGrain.GetBalance();

int secondUserId = 20;
IAccountGrain accountGrain2 = client.GetGrain<IAccountGrain>(secondUserId, secondUserId.ToString());
var balance2 = await accountGrain2.GetBalance();


//IHello friend = client.GetGrain<IHello>(0);
//string response = await friend.SayHello("Hi friend!");

//Console.WriteLine($"""
//    {response}

//    Press any key to exit...
//    """);

Console.ReadKey();

await host.StopAsync();