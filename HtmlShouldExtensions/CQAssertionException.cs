using System;

namespace HtmlShouldExtensions
{
    [Serializable]
    public class CQAssertionException : Exception
    {
        public CQAssertionException() { }
        public CQAssertionException(string message) : base(message) { }
        public CQAssertionException(string message, Exception inner) : base(message, inner) { }
        protected CQAssertionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
