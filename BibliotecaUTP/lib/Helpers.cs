using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaUTP.lib {
    public static class Helpers {
        private const string ACCEPT_HEADER_NAME = "Accept";
        private const string ACCEPT_HEADER_VALUE = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        private const string USER_AGENT_NAME = "User-Agent";
        private const string USER_AGENT_VALUE = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0";

        private static readonly HttpClient _httpClient = new HttpClient(
            new HttpClientHandler {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }
        );

        public static async Task<byte[]> DownloadBytesAsync(string url, CancellationToken cancellationToken, bool autoRedirect = true, string userAgent = USER_AGENT_VALUE) {
            url = System.Net.WebUtility.UrlDecode(url);
            HttpResponseMessage response;
            using (var request = new HttpRequestMessage(HttpMethod.Get, url)) {
                request.Headers.TryAddWithoutValidation(ACCEPT_HEADER_NAME, ACCEPT_HEADER_VALUE);
                request.Headers.TryAddWithoutValidation(USER_AGENT_NAME, userAgent);

                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
            }
            if (!response.IsSuccessStatusCode) {
                var statusCode = (int)response.StatusCode;
                if (autoRedirect && (statusCode == 301 || statusCode == 302 || statusCode == 308)) {
                    url = response.Headers?.Location?.AbsoluteUri ?? url;
                }

                using (var request = new HttpRequestMessage(HttpMethod.Get, url)) {
                    response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
                }
            }
            var content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return content;
        }
    }
}