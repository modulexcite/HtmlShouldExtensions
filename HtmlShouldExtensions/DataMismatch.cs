﻿using System.Collections.Generic;

namespace HtmlShouldExtensions
{
    internal class DataMismatch
    {
        public string DataKey { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }

        private readonly string ErrorMessageFormat = "Expected data-{0} to contain \"{1}\" but it {2}.";

        public DataMismatch(KeyValuePair<string, string> kvp, string actualValue)
        {
            this.DataKey = kvp.Key;
            this.Expected = kvp.Value;
            this.Actual = actualValue;
        }

        public string ErrorMessage()
        {
            var actual = DeriveDescriptionOfActualValue();
            return string.Format(this.ErrorMessageFormat, this.DataKey, this.Expected, actual);
        }

        private string DeriveDescriptionOfActualValue()
        {
            var actual = string.IsNullOrEmpty(this.Actual) ? "was empty" : string.Format("contained \"{0}\"", this.Actual);
            return actual;
        }
    }
}
