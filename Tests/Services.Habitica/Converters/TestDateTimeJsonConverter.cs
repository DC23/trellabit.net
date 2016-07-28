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

        [Fact(Skip = "I need to change the converter anyway")]
        public void WriteJson()
        {
            var expected = DateTime.Now;
            string actual = JsonConvert.SerializeObject(expected, new DateTimeJsonConverter());
            Assert.Equal(expected, DateTime.Parse(actual.ToString()));
        }

        [Fact(Skip = "I need to change the converter anyway")]
        public void ReadJson()
        {
            // OK, we need to get the local time
            var expected = DateTime.Now;
            // But make the JsonConverter see it in the equivalent UTC format
            var expectedUtcString = expected.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);
            var actual = JsonConvert.DeserializeObject<DateTime>(expectedUtcString, new DateTimeJsonConverter());
            Assert.IsType<DateTime>(actual);
            Assert.Equal(expected, actual);
        }
    }
}
