using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (cq.HasClass(cssClass))
            {
                return cq;
            }

            throw new CQAssertionException(string.Format("CSS class {0} not found.", cssClass));
        }

        public static CQ WithData(this CQ cq, IDictionary<string, string> expectedData)
        {
            var dataMismatches = expectedData.Where(kvp => cq.DataRaw(kvp.Key) != kvp.Value)
                                             .Select(kvp => new DataMismatch(kvp, cq.DataRaw(kvp.Key)))
                                             .ToArray();

            if (dataMismatches.Length == 0)
            {
                return cq;
            }

            string errorMessage = CreateWithDataErrorMessage(dataMismatches);
            throw new CQAssertionException(errorMessage);
        }

        public static CQ NumberOfTimes(this CQ cq, int matchCount)
        {
            try
            {
                Assert.Equal(matchCount, cq.Length);
            }
            catch (EqualException e)
            {
                throw new FailedMatchException("Selector matched an unexpected number of times." + e.Message);
            }
            return cq;
        }

        private static string CreateWithDataErrorMessage(DataMismatch[] dataMismatches)
        {
            StringBuilder errorMessage = new StringBuilder("Some data attributes were not present or had unexpected values.");
            errorMessage.AppendLine();
            foreach (var mismatch in dataMismatches)
            {
                errorMessage.AppendLine(mismatch.ErrorMessage());
                errorMessage.AppendLine();
            }
            return errorMessage.ToString();
        }
    }
}
