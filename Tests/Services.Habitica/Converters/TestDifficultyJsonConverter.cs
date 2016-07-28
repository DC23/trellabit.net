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

        [Fact]
        public void ReadJsonTrivial()
        {
            var expected = Difficulty.Trivial;
            var converter = new DifficultyJsonConverter();
            var reader = new Mock<JsonReader>();
            reader.Setup(foo => foo.Value.ToString()).Returns(
                DifficultyJsonConverter.DifficultyValues[expected]);

            Difficulty actual = (Difficulty)converter.ReadJson(
                reader.Object,
                typeof(Difficulty),
                null,
                new JsonSerializer());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadJsonEasy()
        {
            var expected = Difficulty.Easy;
            var converter = new DifficultyJsonConverter();
            var reader = new Mock<JsonReader>();
            reader.Setup(foo => foo.Value.ToString()).Returns(
                DifficultyJsonConverter.DifficultyValues[expected]);

            Difficulty actual = (Difficulty)converter.ReadJson(
                reader.Object,
                typeof(Difficulty),
                null,
                new JsonSerializer());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadJsonMedium()
        {
            var expected = Difficulty.Medium;
            var converter = new DifficultyJsonConverter();
            var reader = new Mock<JsonReader>();
            reader.Setup(foo => foo.Value.ToString()).Returns(
                DifficultyJsonConverter.DifficultyValues[expected]);

            Difficulty actual = (Difficulty)converter.ReadJson(
                reader.Object,
                typeof(Difficulty),
                null,
                new JsonSerializer());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadJsonHard()
        {
            var expected = Difficulty.Hard;
            var converter = new DifficultyJsonConverter();
            var reader = new Mock<JsonReader>();
            reader.Setup(foo => foo.Value.ToString()).Returns(
                DifficultyJsonConverter.DifficultyValues[expected]);

            Difficulty actual = (Difficulty)converter.ReadJson(
                reader.Object,
                typeof(Difficulty),
                null,
                new JsonSerializer());

            Assert.Equal(expected, actual);
        }
    }
}
