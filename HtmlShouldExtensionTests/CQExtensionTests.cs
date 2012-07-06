using System;
using System.Collections.Generic;
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

        [Fact]
        public void WithData_passes_if_data_attributes_present()
        {
            var fragment = CQ.Create("<div data-foo=\"bar\", data-baz=\"42\"></div>");
            Assert.DoesNotThrow(() => fragment.WithData(new Dictionary<string, string> { { "foo", "bar" }, { "baz", "42" } }));
        }

        [Fact]
        public void WithData_fails_if_data_attributes_missing()
        {
            var fragment = CQ.Create("<div data-foo=\"bar\"></div>");
            Assert.Throws<CQAssertionException>(() => fragment.WithData(new Dictionary<string, string> { { "foo", "bar" }, { "baz", "42" } }));
        }

        [Fact]
        public void WithData_provides_an_error_message_showing_mismatched_data_attributes_on_failure()
        {
            var fragment = CQ.Create("<div data-foo=\"bar\"></div>");
            var thrown = Assert.Throws<CQAssertionException>(() => fragment.WithData(new Dictionary<string, string>
            {
                { "foo", "baz" },
                { "baz", "42" }
            }));
            var message = thrown.Message;
            message.ShouldContain("Some data attributes were not present or had unexpected values.");
            message.ShouldContain("Expected data-baz to contain \"42\" but it was empty." + Environment.NewLine);
            message.ShouldContain("Expected data-foo to contain \"baz\" but it contained \"bar\".");
        }
    }
}
