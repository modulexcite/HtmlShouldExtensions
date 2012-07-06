using CsQuery;
using HtmlShouldExtensions;
using Should;
using Xunit;

namespace HtmlShouldExtensionTests
{
    public class CQExtensionTests
    {
        [Fact]
        public void WithInnerHtml_passes_if_html_present()
        {
            var fragment = CQ.CreateFragment("<p>lorem ipsum</p>");
            Assert.DoesNotThrow(() => fragment.WithInnerHtml("lorem ipsum"));
        }

        [Fact]
        public void WithInnerHtml_throws_cq_exception_if_html_not_present()
        {
            var fragment = CQ.Create("<p>lorem ipsum</p>");
            var thrown = Assert.Throws<CQAssertionException>(() => fragment.WithInnerHtml("foo"));
            var message = thrown.Message;
            message.ShouldContain("First difference is at position 0");
            message.ShouldContain("Expected: foo");
            message.ShouldContain("Actual:   lorem ipsum");
            message.ShouldContain("InnerHtml did not match");
        }

        [Fact]
        public void WithClass_throws_cq_exception_if_class_not_present()
        {
            var fragment = CQ.Create("<p>lorem ipsum</p>");
            var thrown = Assert.Throws<CQAssertionException>(() => fragment.WithClass("para"));
            thrown.Message.ShouldEqual("CSS class para not found.");
        }

        [Fact]
        public void WithClass_passes_if_class_present()
        {
            var fragment = CQ.Create("<p class=\"para\">lorem ipsum</p>");
            Assert.DoesNotThrow(() => fragment.WithClass("para"));
        }
    }
}
