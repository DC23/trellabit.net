using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellabit.Services.Habitica.Converters;
using Xunit;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestGuidConverter
    {
        [Fact]
        public void CanConvertString()
        {
            Assert.True(new GuidJsonConverter().CanConvert(typeof(string)));
        }

        [Fact]
        public void CanConvertGuid()
        {
            Assert.True(new GuidJsonConverter().CanConvert(typeof(Guid)));
        }

        [Fact]
        public void WriteStringToJson()
        {
            // arrange
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();
            var converter = new GuidJsonConverter();
            var guid = Guid.NewGuid();
            string expected = guid.ToString();

            // act
            converter.WriteJson(writer, expected, serializer);

            // assert
            Assert.Equal(expected, stringWriter.ToString());
        }

        [Fact]
        public void WriteGuidToJson()
        {
            // arrange
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();
            var converter = new GuidJsonConverter();
            var guid = Guid.NewGuid();
            string expected = guid.ToString();

            // act
            converter.WriteJson(writer, guid, serializer);

            // assert
            Assert.Equal(expected, stringWriter.ToString());
        }

        [Fact]
        public void ReadJson()
        {
            // arrange
            var converter = new GuidJsonConverter();
            var expected = Guid.NewGuid();
            var serializer = new JsonSerializer();
            var reader = new Mock<JsonReader>();
            reader.Setup(foo => foo.Value.ToString()).Returns(expected.ToString());

            // act
            var actual = converter.ReadJson(reader.Object, typeof(string), null, serializer);

            // assert
            Assert.IsType<Guid>(actual);
            Assert.Equal(expected, actual);
        }
    }
}
