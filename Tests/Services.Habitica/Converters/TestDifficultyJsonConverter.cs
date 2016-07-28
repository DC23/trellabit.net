using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using Trellabit.Services;
using Trellabit.Services.Habitica.Converters;
using Xunit;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestDifficultyJsonConverter
    {
        [Fact]
        public void CanConvertDifficult()
        {
            Assert.True(new DifficultyJsonConverter().CanConvert(typeof(Difficulty)));
        }

        private void ToJson(Difficulty expected)
        {
            string actual = JsonConvert.SerializeObject(expected, new DifficultyJsonConverter());
            Assert.Equal(DifficultyJsonConverter.DifficultyValues[expected], actual);
        }

        private void FromJson(Difficulty expected)
        {
            string expectedValue = DifficultyJsonConverter.DifficultyValues[expected];
            string json = $"'{expectedValue}'";
            var actual = JsonConvert.DeserializeObject<Difficulty>(json, new DifficultyJsonConverter());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WriteJsonTrivial()
        {
            ToJson(Difficulty.Trivial);
        }

        [Fact]
        public void WriteJsonEasy()
        {
            ToJson(Difficulty.Easy);
        }

        [Fact]
        public void WriteJsonMedium()
        {
            ToJson(Difficulty.Medium);
        }

        [Fact]
        public void WriteJsonHard()
        {
            ToJson(Difficulty.Hard);
        }

        [Fact]
        public void ReadJsonTrivial()
        {
            FromJson(Difficulty.Trivial);
        }

        [Fact]
        public void ReadJsonEasy()
        {
            FromJson(Difficulty.Easy);
        }

        [Fact]
        public void ReadJsonMedium()
        {
            FromJson(Difficulty.Medium);
        }

        [Fact]
        public void ReadJsonHard()
        {
            FromJson(Difficulty.Hard);
        }
    }
}
