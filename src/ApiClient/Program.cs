using ApiClient;

var builder = WebApplication.CreateBuilder(args);


var dbConnectionString = "Server=localhost;Database=OrleansStorage;User=sa;Password=Akjd3_as77;";
var invariant = "System.Data.SqlClient";
// Add services to the container.
builder.Host.UseOrleansClient(client =>
 {
     client.UseAdoNetClustering(options =>
     {
         options.Invariant = invariant;
         options.ConnectionString = dbConnectionString;
     });
 })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();


//Add hosted service to send notifications from queue
builder.Services.AddHostedService<NotificationSenderHostedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
