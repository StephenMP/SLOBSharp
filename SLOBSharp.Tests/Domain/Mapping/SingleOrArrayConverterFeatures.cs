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
        public void CanConvertSingleResult()
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
        public void CanConvertAnArrayOfResults()
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
    }
}
