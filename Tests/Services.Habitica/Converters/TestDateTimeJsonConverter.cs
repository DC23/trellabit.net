using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trellabit.Services.Habitica.Converters;
using Xunit;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestDateTimeJsonConverter
    {
        [Fact]
        public void CanConvertDateTime()
        {
            Assert.True(new DateTimeJsonConverter().CanConvert(typeof(DateTime)));
        }

        [Fact]
        public void WriteJson()
        {
            // arrange
            var stringWriter = new StringWriter();
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();
            var converter = new DateTimeJsonConverter();
            var expected = DateTime.Now;

            // act
            converter.WriteJson(writer, expected, serializer);

            // assert
            Assert.Equal(expected, DateTime.Parse(stringWriter.ToString()));
        }

        [Fact]
        public void ReadJson()
        {
            var converter = new DateTimeJsonConverter();
            var serializer = new JsonSerializer();
            var reader = new Mock<JsonReader>();

            // OK, we need to get the local time
            var expected = DateTime.Now;
            // But make the JsonConverter see it in the equivalent UTC format
            var expectedUtc = expected.ToUniversalTime();
            reader.Setup(foo => foo.Value.ToString()).Returns(
                expectedUtc.ToString("o", CultureInfo.InvariantCulture));

            DateTime actual = (DateTime)converter.ReadJson(reader.Object, typeof(DateTime), null, serializer);

            Assert.IsType<DateTime>(actual);
            //Assert.Equal(expectedString, actual.ToString("o", CultureInfo.InvariantCulture));
            Assert.Equal(expected, actual);
        }
    }
}
