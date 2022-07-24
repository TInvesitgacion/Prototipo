using System.Collections.Generic;
using System.Linq;

namespace BibliotecaUTP.lib {
    public class Feed {
        public FeedType Type { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public IList<FeedItem> Items { get; set; }

        public string totalResults { get; set; }

        public string startIndex { get; set; }

        public string itemsPerPage { get; set; }

        public Feed() {
        }

        public Feed(BaseFeed feed) {
            Title = feed.Title;
            Link = feed.Link;

            Items = feed.Items.Select(x => x.ToFeedItem()).ToList();
        }
    }
}
