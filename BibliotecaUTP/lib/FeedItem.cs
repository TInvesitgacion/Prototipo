namespace BibliotecaUTP.lib {
    public class FeedItem {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string identifier { get; internal set; }
        public string imagen { get; internal set; }

        public FeedItem() {
        }

        public FeedItem(BaseFeedItem feedItem) {
            this.Title = feedItem.Title;
            this.Link = feedItem.Link;
        }
    }
}
