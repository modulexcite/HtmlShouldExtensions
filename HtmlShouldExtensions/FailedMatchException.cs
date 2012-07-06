using System;
namespace HtmlShouldExtensions
{
    [Serializable]
    public class FailedMatchException : Exception
    {
        public FailedMatchException() { }
        public FailedMatchException(string message) : base(message) { }
        public FailedMatchException(string message, Exception inner) : base(message, inner) { }
        protected FailedMatchException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
