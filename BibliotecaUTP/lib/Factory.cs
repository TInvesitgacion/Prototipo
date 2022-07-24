namespace BibliotecaUTP.lib {
    internal static class Factory {
        public static AbstractXmlFeedParser GetParser(FeedType feedType) {
            switch (feedType) {
                default: return new Rss20Parser();
            }
        }
    }
}
