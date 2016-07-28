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

        [Fact]
        public void WriteJsonTrivial()
        {
            var stringWriter = new StringWriter();
            var converter = new DifficultyJsonConverter();
            var expected = Difficulty.Trivial;

            converter.WriteJson(new JsonTextWriter(stringWriter), expected, new JsonSerializer());

            Assert.Equal(DifficultyJsonConverter.DifficultyValues[expected], stringWriter.ToString());
        }

        [Fact]
        public void WriteJsonEasy()
        {
            var stringWriter = new StringWriter();
            var converter = new DifficultyJsonConverter();
            var expected = Difficulty.Easy;

            converter.WriteJson(new JsonTextWriter(stringWriter), expected, new JsonSerializer());

            Assert.Equal(DifficultyJsonConverter.DifficultyValues[expected], stringWriter.ToString());
        }

        [Fact]
        public void WriteJsonMedium()
        {
            var stringWriter = new StringWriter();
            var converter = new DifficultyJsonConverter();
            var expected = Difficulty.Medium;

            converter.WriteJson(new JsonTextWriter(stringWriter), expected, new JsonSerializer());

            Assert.Equal(DifficultyJsonConverter.DifficultyValues[expected], stringWriter.ToString());
        }

        [Fact]
        public void WriteJsonHard()
        {
            var stringWriter = new StringWriter();
            var converter = new DifficultyJsonConverter();
            var expected = Difficulty.Hard;

            converter.WriteJson(new JsonTextWriter(stringWriter), expected, new JsonSerializer());

            Assert.Equal(DifficultyJsonConverter.DifficultyValues[expected], stringWriter.ToString());
        }

        private void FromJson(Difficulty expected)
        {
            string expectedValue = DifficultyJsonConverter.DifficultyValues[expected];
            string json = $"'{expectedValue}'";

            var actual = JsonConvert.DeserializeObject<Difficulty>(json, new DifficultyJsonConverter());

            Assert.Equal(expected, actual);
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
