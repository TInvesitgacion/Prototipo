using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    public class Rss20Feed : BaseFeed {
        public string Description { get; set; }

        public string title { get; private set; }

        public string link { get; private set; }

        public string totalResults { get; private set; }

        public string startIndex { get; private set; }

        public string itemsPerPage { get; private set; }

        public Rss20Feed()
            : base() {
        }

        public Rss20Feed(string feedXml, XElement channel) : base(feedXml, channel) {
            this.title = channel.GetValue("title");
            this.link = channel.GetValue("link");
            this.totalResults = channel.GetValue("opensearch:totalResults");
            this.startIndex = channel.GetValue("opensearch:startIndex");
            this.itemsPerPage = channel.GetValue("opensearch:itemsPerPage");
            this.Description = channel.GetValue("description");

            var items = channel.GetElements("item");

            foreach (var item in items) {
                this.Items.Add(new Rss20FeedItem(item));
            }
        }

        public override Feed ToFeed() {
            Feed f = new Feed(this) {
                Type = FeedType.Rss_2_0,
                totalResults = this.totalResults,
                startIndex = this.startIndex,
                itemsPerPage = this.itemsPerPage,
                Description = this.Description
            };
            return f;
        }
    }
}
