using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    internal class Rss20Parser : AbstractXmlFeedParser {

        public override BaseFeed Parse(string feedXml, XDocument feedDoc) {
            var rss = feedDoc.Root;
            var channel = rss.GetElement("channel");
            Rss20Feed feed = new Rss20Feed(feedXml, channel);
            return feed;
        }
    }
}
