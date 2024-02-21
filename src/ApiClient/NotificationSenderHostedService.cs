
using GrainInterfaces;

namespace ApiClient
{
    public class NotificationSenderHostedService : BackgroundService
    {

        private IClusterClient _orleansClient;
        private readonly ILogger<NotificationSenderHostedService> _logger;
        public NotificationSenderHostedService(IClusterClient clusterClient, ILogger<NotificationSenderHostedService> logger)
        {
            _orleansClient = clusterClient;
            _logger = logger;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // When the timer should have no due-time, then do the work once now.
            await SendNotifications(stoppingToken);

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await SendNotifications(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        public async Task SendNotifications(CancellationToken cancellationToken)
        {
            var notification = _orleansClient.GetGrain<INotificationGrain>("");

            await notification.ProcessNotifications();


        }
   
    }
}
