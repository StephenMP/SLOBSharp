using System;
using SLOBSharp.Domain.Extensions;
using SLOBSharp.Tests.TestingResources;
using Xunit;

namespace SLOBSharp.Tests.Domain.Extensions
{
    internal class StringExtensionsSteps
    {
        private SingleOrArrayDto dto;
        private string dtoString;
        private string stringResult;

        internal void GivenIHaveAJsonStringForASingleOrArrayDtoString()
        {
            this.dtoString = $"{{result: \"{this.stringResult}\"}}";
        }

        internal void GivenIHaveAStringResult()
        {
            this.stringResult = Guid.NewGuid().ToString();
        }

        internal void WhenICallJsonToObjectExtension()
        {
            this.dto = this.dtoString.JsonToObject<SingleOrArrayDto>();
        }

        internal void ThenIShouldHaveAResultObject()
        {
            Assert.NotNull(this.dto);
        }

        internal void ThenTheResultsShouldDeserializeCorrectly()
        {
            Assert.Contains(this.stringResult, this.dto.Result[0]);
        }
    }
}