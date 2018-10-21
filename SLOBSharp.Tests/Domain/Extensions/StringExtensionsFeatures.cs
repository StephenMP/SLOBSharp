using Xunit;

namespace SLOBSharp.Tests.Domain.Extensions
{
    public class StringExtensionsFeatures
    {
        private readonly StringExtensionsSteps steps;

        public StringExtensionsFeatures()
        {
            this.steps = new StringExtensionsSteps();
        }

        [Fact]
        public void CanConvertFromJsonToObject()
        {
            this.steps.GivenIHaveAStringResult();
            this.steps.GivenIHaveAJsonStringForASingleOrArrayDtoString();

            this.steps.WhenICallJsonToObjectExtension();

            this.steps.ThenIShouldHaveAResultObject();
            this.steps.ThenTheResultsShouldDeserializeCorrectly();
        }
    }
}
