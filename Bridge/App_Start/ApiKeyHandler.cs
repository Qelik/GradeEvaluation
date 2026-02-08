using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class ApiKeyHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var path = request.RequestUri?.AbsolutePath?.ToLowerInvariant() ?? string.Empty;

        // Allow swagger UI and swagger resources (and the HelpPage) to be served without an API key
        if (path.StartsWith("/swagger") || path.Contains("/swagger/") || path.StartsWith("/api/help") || path.StartsWith("/help"))
        {
            return base.SendAsync(request, cancellationToken);
        }

        // Allow CORS preflight requests
        if (request.Method == HttpMethod.Options)
        {
            return base.SendAsync(request, cancellationToken);
        }

        if (!request.Headers.TryGetValues("X-Api-Key", out var values))
        {
            return Task.FromResult(request.CreateResponse(HttpStatusCode.Unauthorized, "Missing API key"));
        }

        var provided = values.FirstOrDefault();
        var expected = ConfigurationManager.AppSettings["ApiKey"];

        if (string.IsNullOrEmpty(expected) || provided != expected)
        {
            return Task.FromResult(request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid API key"));
        }

        return base.SendAsync(request, cancellationToken);
    }
}