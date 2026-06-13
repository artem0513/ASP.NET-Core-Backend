using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.WebApi
{
    public class Worker : BackgroundService
    {
        private const string Url = "https://artem0513.github.io/artem-page/";
        private static readonly TimeSpan Delay = TimeSpan.FromSeconds(10);

        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;

        public Worker(
            ILogger<Worker> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckWebsiteAsync(stoppingToken);
                await Task.Delay(Delay, stoppingToken);
            }
        }

        private async Task CheckWebsiteAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var response = await _httpClient.GetAsync(Url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation(
                        "{Url} is running. Status code: {StatusCode}",
                        Url,
                        response.StatusCode);
                }
                else
                {
                    _logger.LogWarning(
                        "{Url} is down. Status code: {StatusCode}",
                        Url,
                        response.StatusCode);
                }

                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to check {Url}", Url);
            }
        }
    }
}