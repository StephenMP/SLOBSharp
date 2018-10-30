using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SLOBSharp.Domain.Mapping;
using SLOBSharp.Tests.TestingResources;
using Xunit;

namespace SLOBSharp.Tests.Domain.Mapping
{
    internal class SingleOrArrayConverterSteps
    {
        private readonly List<string> results;
        private bool canConvert;
        private SingleOrArrayConverter<string> converter;
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

        internal void GivenIHaveASingleOrArrayConverter()
        {
            this.converter = new SingleOrArrayConverter<string>();
        }

        internal void GivenIHaveASingleOrArrayDto()
        {
            this.singleOrArrayDtoResult = new SingleOrArrayDto();
        }

        internal void GivenIHaveASingleOrArrayDtoResultValue()
        {
            this.singleOrArrayDtoResult.Result.Add(Guid.NewGuid().ToString());
        }

        internal void GivenIHaveJsonWithASingleResult()
        {
            this.resultJson = $"{{result: \"{this.results[0]}\"}}";
        }

        internal void GivenIHaveJsonWithMultipleResults()
        {
            var arrayJson = JsonConvert.SerializeObject(this.results);
            this.resultJson = $"{{result:{arrayJson}}}";
        }

        internal void ThenIShouldBeAbleToConvertListTypes()
        {
            Assert.True(this.canConvert);
        }

        internal void ThenIShouldHaveAResultDto()
        {
            Assert.NotNull(this.singleOrArrayDtoResult);
        }

        internal void ThenMyResultDtoShouldHaveResults()
        {
            Assert.NotNull(this.singleOrArrayDtoResult.Result);
            Assert.NotEmpty(this.singleOrArrayDtoResult.Result);
        }

        internal void ThenMyResultDtoShouldHaveTheSameNumberOfResults()
        {
            Assert.Equal(this.results.Count, this.singleOrArrayDtoResult.Result.Count);
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

        internal void ThenTheResultingJsonStringShouldContainTheDtoValues()
        {
            foreach (var result in this.singleOrArrayDtoResult.Result)
            {
                Assert.Contains(result, this.resultJson);
            }
        }

        internal void ThenTheResultingJsonStringShouldHaveAValue()
        {
            Assert.NotNull(this.resultJson);
            Assert.NotEmpty(this.resultJson);
        }

        internal void WhenIAskToConvertListType()
        {
            this.canConvert = this.converter.CanConvert(typeof(List<string>));
        }

        internal void WhenIDeserializeTheJson()
        {
            this.singleOrArrayDtoResult = JsonConvert.DeserializeObject<SingleOrArrayDto>(this.resultJson);
        }

        internal void WhenISerializeTheDto()
        {
            this.resultJson = JsonConvert.SerializeObject(this.singleOrArrayDtoResult);
        }
    }
}
