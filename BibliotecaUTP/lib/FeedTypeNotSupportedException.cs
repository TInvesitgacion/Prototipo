using System;

namespace BibliotecaUTP.lib {
    public class FeedTypeNotSupportedException : Exception {

        public FeedTypeNotSupportedException() {
        }

        public FeedTypeNotSupportedException(string message) : base(message) {
        }

        public FeedTypeNotSupportedException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
