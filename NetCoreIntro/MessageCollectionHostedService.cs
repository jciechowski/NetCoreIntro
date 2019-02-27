using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreIntro
{
    public class CustomHostedService : IHostedService
    {
        private readonly LocalStore _messageReceiver;
        private readonly ILogger<CustomHostedService> _logger;

        public CustomHostedService(LocalStore messageReceiver, ILogger<CustomHostedService> logger)
        {
            _messageReceiver = messageReceiver;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service is listening.");

            _messageReceiver.Messages.CollectionChanged += DoWork;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageReceiver.Messages.CollectionChanged -= DoWork;

            return Task.CompletedTask;
        }

        private void DoWork(object sender, NotifyCollectionChangedEventArgs e)
        {
            _logger.LogInformation("Did some important work!");
        }
    }
}