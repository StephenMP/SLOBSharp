using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SLOBSharp.Tests.TestingResources;
using Xunit;

namespace SLOBSharp.Tests.Domain.Mapping
{
    internal class SingleOrArrayConverterSteps
    {
        private readonly List<string> results;
        private string resultJson;
        private SingleOrArrayDto singleOrArrayDtoResult;

        public SingleOrArrayConverterSteps()
        {
            this.results = new List<string>();
        }

        internal void GivenIHaveAResultString()
        {
            this.results.Add(Guid.NewGuid().ToString());
        }

        internal void ThenMyResultDtoShouldHaveResults()
        {
            Assert.NotNull(this.singleOrArrayDtoResult.Result);
            Assert.NotEmpty(this.singleOrArrayDtoResult.Result);
        }

        internal void ThenTheResultDtoResultsShouldEqualTheJsonResults()
        {
            for (var i = 0; i < this.results.Count; i++)
            {
                var expectedResult = this.results[i];
                var actualResult = this.singleOrArrayDtoResult.Result[i];
                Assert.Equal(expectedResult, actualResult);
            }
        }

        internal void GivenIHaveJsonWithMultipleResults()
        {
            var arrayJson = JsonConvert.SerializeObject(this.results);
            this.resultJson = $"{{result:{arrayJson}}}";
        }

        internal void ThenMyResultDtoShouldHaveTheSameNumberOfResults()
        {
            Assert.Equal(this.results.Count, this.singleOrArrayDtoResult.Result.Count);
        }

        internal void ThenIShouldHaveAResultDto()
        {
            Assert.NotNull(this.singleOrArrayDtoResult);
        }

        internal void GivenIHaveJsonWithASingleResult()
        {
            this.resultJson = $"{{result: \"{this.results[0]}\"}}";
        }

        internal void WhenIDeserializeTheJson()
        {
            this.singleOrArrayDtoResult = JsonConvert.DeserializeObject<SingleOrArrayDto>(this.resultJson);
        }
    }
}