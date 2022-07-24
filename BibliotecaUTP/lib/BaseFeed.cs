using System.Collections.Generic;
using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    public abstract class BaseFeed {
        public abstract Feed ToFeed();

        public string Title { get; set; }

        public string Link { get; set; }

        public ICollection<BaseFeedItem> Items { get; set; }

        public string OriginalDocument { get; private set; }

        public XElement Element { get; }

        protected BaseFeed() {
            this.Items = new List<BaseFeedItem>();
        }

        protected BaseFeed(string feedXml, XElement channel) : this() {
            this.OriginalDocument = feedXml;

            this.Title = channel.GetValue("title");
            this.Link = channel.GetValue("link");
            this.Element = channel;
        }
    }
}
