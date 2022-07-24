using System;
using System.Text;
using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    internal static class FeedParser {
        public static FeedType ParseFeedType(XDocument doc) {
            string rootElement = doc.Root.Name.LocalName;
            if (rootElement.EqualsIgnoreCase("rss")) {
                string version = doc.Root.Attribute("version").Value;
                if (version.EqualsIgnoreCase("2.0")) {
                    if (doc.Root.Attribute(XName.Get("media", XNamespace.Xmlns.NamespaceName)) != null) {
                        return FeedType.MediaRss;
                    } else {
                        return FeedType.Rss_2_0;
                    }
                }
            }

            throw new FeedTypeNotSupportedException($"unknown feed type {rootElement}");
        }

        public static Feed GetFeed(byte[] feedContentData) {
            string feedContent = Encoding.UTF8.GetString(feedContentData);
            feedContent = RemoveWrongChars(feedContent);

            XDocument feedDoc = XDocument.Parse(feedContent);

            Encoding encoding = GetEncoding(feedDoc);

            if (encoding != Encoding.UTF8) {
                feedContent = encoding.GetString(feedContentData);
                feedContent = RemoveWrongChars(feedContent);
            }

            var feedType = ParseFeedType(feedDoc);

            var parser = Factory.GetParser(feedType);
            var feed = parser.Parse(feedContent);

            return feed.ToFeed();
        }

        private static Encoding GetEncoding(XDocument feedDoc) {
            Encoding encoding = Encoding.UTF8;

            try {
                var encodingStr = feedDoc.Document.Declaration?.Encoding;
                if (!string.IsNullOrEmpty(encodingStr))
                    encoding = Encoding.GetEncoding(encodingStr);

            } catch (Exception) { }
            return encoding;
        }

        private static string RemoveWrongChars(string feedContent) {
            for (int charCode = 0; charCode <= 31; charCode++) {
                if (charCode == 0x0D || charCode == 0x0A || charCode == 0x09) continue;

                feedContent = feedContent.Replace(((char)charCode).ToString(), string.Empty);
            }

            feedContent = feedContent.Replace(((char)127).ToString(), string.Empty);
            feedContent = feedContent.Replace(((char)65279).ToString(), string.Empty);

            return feedContent.Trim();
        }
    }
}
