using System;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaUTP.lib {
    public static class FeedReader {
        public static string GetAbsoluteUrl(string url) {
            return new UriBuilder(url).ToString();
        }

        public static async Task<Feed> ReadAsync(string url, CancellationToken cancellationToken, bool autoRedirect = true, string userAgent = null) {
            var feedContent = await Helpers.DownloadBytesAsync(GetAbsoluteUrl(url), cancellationToken, autoRedirect, userAgent).ConfigureAwait(false);
            return ReadFromByteArray(feedContent);
        }

        public static Task<Feed> ReadAsync(string url, bool autoRedirect = true, string userAgent = null) {
            return ReadAsync(url, CancellationToken.None, autoRedirect, userAgent);
        }

        public static Feed ReadFromByteArray(byte[] feedContent) {
            return FeedParser.GetFeed(feedContent);
        }
    }
}