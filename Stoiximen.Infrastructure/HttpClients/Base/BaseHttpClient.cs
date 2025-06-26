using Microsoft.Extensions.Logging;
using Stoiximen.Infrastructure.HttpClients.Config;
using System.Text;
using System.Text.Json;

namespace Stoiximen.Infrastructure.HttpClients.Base
{
    public abstract class BaseHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        protected readonly ILogger _logger;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseHttpClient(IHttpClientFactory httpClientFactory, IHttpClientConfiguration configuration, string clientName, ILogger logger)
        {
            _httpClient = httpClientFactory?.CreateClient(clientName) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.BaseAddress = new Uri(configuration.BaseUrl ?? throw new ArgumentNullException(nameof(configuration.BaseUrl)));
            _httpClient.Timeout = configuration.Timeout;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        protected async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Making GET request to: {Endpoint}", endpoint);

                using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);

                _logger.LogDebug("GET request successful for: {Endpoint}", endpoint);
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error during GET request to: {Endpoint}", endpoint);
                throw;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError(ex, "Timeout during GET request to: {Endpoint}", endpoint);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error for GET request to: {Endpoint}", endpoint);
                throw;
            }
        }

        protected async Task<T> PostAsync<T>(string endpoint, object? data = null, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Making POST request to: {Endpoint}", endpoint);

                HttpContent? content = null;
                if (data != null)
                {
                    var json = JsonSerializer.Serialize(data, _jsonOptions);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                using var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);

                _logger.LogDebug("POST request successful for: {Endpoint}", endpoint);
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error during POST request to: {Endpoint}", endpoint);
                throw;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError(ex, "Timeout during POST request to: {Endpoint}", endpoint);
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error for POST request to: {Endpoint}", endpoint);
                throw;
            }
        }

        protected async Task<string> GetStringAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Making GET string request to: {Endpoint}", endpoint);

                using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogDebug("GET string request successful for: {Endpoint}", endpoint);

                return content;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error during GET string request to: {Endpoint}", endpoint);
                throw;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                _logger.LogError(ex, "Timeout during GET string request to: {Endpoint}", endpoint);
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
        }
    }
}