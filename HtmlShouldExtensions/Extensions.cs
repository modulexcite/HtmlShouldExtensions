using CsQuery;
using Should.Core.Assertions;
using Should.Core.Exceptions;

namespace HtmlShouldExtensions
{
    public static class Extensions
    {
        public static CQ ShouldMatchSelector(this string mvcString, string selector)
        {
            var fragment = CQ.CreateFragment(mvcString.ToString());
            var match = fragment.Select(selector);
            if (match.Length == 0)
            {
                throw new FailedMatchException("Selector did not match");
            }
            return match;
        }

        public static CQ WithInnerHtml(this CQ cq, string innerHtml)
        {
            try
            {
                Assert.Equal(innerHtml, cq.Html());
            }
            catch (EqualException e)
            {
                string message = "InnerHtml did not match. " + e.Message;
                throw new CQAssertionException(message);
            }
            return cq;
        }

        public static CQ WithClass(this CQ cq, string cssClass)
        {
            try
            {
                Assert.True(cq.HasClass(cssClass));
            }
            catch (TrueException)
            {
                throw new CQAssertionException(string.Format("CSS class {0} not found.", cssClass));
            }
            return cq;
        }
    }
}
