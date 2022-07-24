using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    public abstract class BaseFeedItem {
        public string Title { get; set; }

        public string Link { get; set; }

        public XElement Element { get; }

        internal abstract FeedItem ToFeedItem();

        protected BaseFeedItem() {
        }

        protected BaseFeedItem(XElement item) {
            this.Title = item.GetValue("title");
            this.Link = item.GetValue("link");
            this.Element = item;
        }
    }
}
