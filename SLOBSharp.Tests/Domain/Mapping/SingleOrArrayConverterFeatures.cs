using System;
using Xunit;

namespace SLOBSharp.Tests.Domain.Mapping
{
    public class SingleOrArrayConverterFeatures
    {
        private readonly SingleOrArrayConverterSteps steps;

        public SingleOrArrayConverterFeatures()
        {
            this.steps = new SingleOrArrayConverterSteps();
        }

        [Fact]
        public void CanConvertListTypes()
        {
            this.steps.GivenIHaveASingleOrArrayConverter();

            this.steps.WhenIAskToConvertListType();

            this.steps.ThenIShouldBeAbleToConvertListTypes();
        }

        [Fact]
        public void CanCReadASingleResult()
        {
            this.steps.GivenIHaveAResultString();
            this.steps.GivenIHaveJsonWithASingleResult();

            this.steps.WhenIDeserializeTheJson();

            this.steps.ThenIShouldHaveAResultDto();
            this.steps.ThenMyResultDtoShouldHaveResults();
            this.steps.ThenMyResultDtoShouldHaveTheSameNumberOfResults();
            this.steps.ThenTheResultDtoResultsShouldEqualTheJsonResults();
        }

        [Fact]
        public void CanCWriteAnArrayOfResults()
        {
            this.steps.GivenIHaveASingleOrArrayDto();
            for (var i = 0; i < new Random().Next(1, 20); i++)
            {
                this.steps.GivenIHaveASingleOrArrayDtoResultValue();
            }

            this.steps.WhenISerializeTheDto();

            this.steps.ThenTheResultingJsonStringShouldHaveAValue();
            this.steps.ThenTheResultingJsonStringShouldContainTheDtoValues();
        }

        [Fact]
        public void CanReadAnArrayOfResults()
        {
            for (var i = 0; i < new Random().Next(1, 20); i++)
            {
                this.steps.GivenIHaveAResultString();
            }

            this.steps.GivenIHaveJsonWithMultipleResults();

            this.steps.WhenIDeserializeTheJson();

            this.steps.ThenIShouldHaveAResultDto();
            this.steps.ThenMyResultDtoShouldHaveResults();
            this.steps.ThenMyResultDtoShouldHaveTheSameNumberOfResults();
            this.steps.ThenTheResultDtoResultsShouldEqualTheJsonResults();
        }

        [Fact]
        public void CanWriteASingleResult()
        {
            this.steps.GivenIHaveASingleOrArrayDto();
            this.steps.GivenIHaveASingleOrArrayDtoResultValue();

            this.steps.WhenISerializeTheDto();

            this.steps.ThenTheResultingJsonStringShouldHaveAValue();
            this.steps.ThenTheResultingJsonStringShouldContainTheDtoValues();
        }
    }
}
