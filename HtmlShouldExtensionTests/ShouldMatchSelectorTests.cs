using Xunit;
using HtmlShouldExtensions;
using System;
using Should;
using CsQuery;

namespace HtmlShouldExtensionTests
{
    public class ShouldMatchSelectorTests
    {
        [Fact]
        public void ShouldMatchSelector_extension_passes_if_selector_matches_at_least_once()
        {
            string fragment = "<p>lorem ipsum</p>";
            fragment.ShouldMatchSelector("p");
        }

        [Fact]
        public void ShouldMatchSelector_extension_fails_with_FailedMatchException_if_selector_does_not_match_at_all()
        {
            string fragment = "<p>lorem ipsum</p>";
            Assert.Throws<FailedMatchException>(() => fragment.ShouldMatchSelector("a"));
        }

        [Fact]
        public void ShouldMatchSelector_extension_returns_a_CQ_object_containing_the_matches()
        {
            string fragment = "<p>lorem ipsum</p>";
            CQ result = fragment.ShouldMatchSelector("p");
            result.ShouldNotBeNull();
            result.Length.ShouldEqual(1);
        }
    }
}
