﻿using Xunit;
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
            Assert.DoesNotThrow(() => fragment.ShouldMatchSelector("p"));
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

        [Fact]
        public void NumberOfTimes_passes_if_selector_matches_that_number_of_times()
        {
            string fragment = "<div><p>para1</p><p>para2</p></div>";
            Assert.DoesNotThrow(() => fragment.ShouldMatchSelector("p").NumberOfTimes(2));
        }

        [Fact]
        public void NumberOfTimes_fails_with_an_appropriate_message_if_selector_matches_an_unexpected_number_of_times()
        {
            string fragment = "<div><p>para1</p><p>para2</p></div>";
            var thrown = Assert.Throws<FailedMatchException>(() => fragment.ShouldMatchSelector("p").NumberOfTimes(1));
            string message = thrown.Message;
            message.ShouldContain("Selector matched an unexpected number of times");
            message.ShouldContain("Expected: 1");
            message.ShouldContain("Actual:   2");
        }

        [Fact]
        public void Once_convenience_method_passes_if_exactly_one_match()
        {
            string fragment = "<div><p>para1</p></div>";
            Assert.DoesNotThrow(() => fragment.ShouldMatchSelector("p").Once());
        }

        [Fact]
        public void Twice_convenience_method_passes_if_exactly_two_matches()
        {
            string fragment = "<div><p>para1</p><p>para2</p></div>";
            Assert.DoesNotThrow(() => fragment.ShouldMatchSelector("p").Twice());
        }
    }
}
