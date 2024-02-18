
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Net;

using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;
using Silo;

Console.WriteLine("Hello, World!");


IHostBuilder builder = Host.CreateDefaultBuilder(args)
  .UseOrleans((context, siloBuilder) =>
  {
      siloBuilder
          .UseLocalhostClustering()
          .ConfigureLogging(logging => logging.AddConsole());
  })
      .UseConsoleLifetime();


var dbConnectionString = "Server=localhost;Database=OrleansStorage;User=sa;Password=Akjd3_as77;";
var invariant = "System.Data.SqlClient";
builder.UseOrleans((context, siloBuilder) =>
{
    siloBuilder.ConfigureLogging(logging => logging.AddConsole());

    siloBuilder.UseTransactions();

    // Конфигурация портов, удалите если хоститесь на разных машинах.
    // Было добавлено для проверки кластера локально
    var (gatewayPort, siloPort) = SiloPortConfigurator.GetAvailablePorts();
    Console.WriteLine($"Silo port: {gatewayPort} - {siloPort}");
    siloBuilder.ConfigureEndpoints(siloPort, gatewayPort);

    // Конфигурация кластеризации
    siloBuilder.UseAdoNetClustering(options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = dbConnectionString;
        
    });

    // Конфигурация хранения состояния, можно заменить на блоб-хранилище
    siloBuilder.AddAdoNetGrainStorage("OrleansStorage", options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = dbConnectionString;
        });

    // Конфигурация напоминаний
    siloBuilder.UseAdoNetReminderService(options =>
    {
        options.Invariant = invariant;
        options.ConnectionString = dbConnectionString;
        });

    // Указание на локальный адрес для кластерного коммуникационного канала
    siloBuilder.Configure<EndpointOptions>(options =>
        options.AdvertisedIPAddress = IPAddress.Loopback);
})
    .UseConsoleLifetime();


using IHost host = builder.Build();

await host.RunAsync();