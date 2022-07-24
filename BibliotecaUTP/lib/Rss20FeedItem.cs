using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    public class Rss20FeedItem : BaseFeedItem {
        public string Description { get; set; }

        public string Guid { get; set; }

        public string title { get; private set; }

        public string identifier { get; private set; }

        public string link { get; private set; }

        public string imagen { get; private set; }

        public Rss20FeedItem()
            : base() {
        }

        public Rss20FeedItem(XElement item) : base(item) {
            this.title = item.GetValue("title");
            this.identifier = item.GetValue("dc:identifier");
            this.link = item.GetValue("link");

            this.Description = item.GetValue("description");
            this.imagen = item.GetImgValue("description");
            this.Guid = item.GetValue("guid");
        }

        internal override FeedItem ToFeedItem() {
            FeedItem fi = new FeedItem(this) {
                Description = this.Description,
                Id = this.Guid,

                identifier = this.identifier,
                imagen = this.imagen
            };
            return fi;
        }
    }
}
