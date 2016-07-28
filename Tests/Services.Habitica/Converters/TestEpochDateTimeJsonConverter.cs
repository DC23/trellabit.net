using Newtonsoft.Json;
using System;
using System.Globalization;
using Trellabit.Services.Habitica.Converters;
using Xunit;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestEpochDateTimeJsonConverter
    {
        [Fact]
        public void CanConvertDateTime()
        {
            Assert.True(new EpochDateTimeJsonConverter().CanConvert(typeof(DateTime)));
        }

        [Fact]
        public void WriteJson()
        {
            long ms = 1469601694391;
            DateTime expected = EpochDateTimeJsonConverter.Epoch.AddMilliseconds(ms);
            string actual = JsonConvert.SerializeObject(expected, new EpochDateTimeJsonConverter());
            Assert.Equal(ms, long.Parse(actual));
        }

        [Fact]
        public void ReadIso8601()
        {
            var expected = DateTime.Now.ToUniversalTime();
            var expectedUtcString = $"'{expected.ToString("o", CultureInfo.InvariantCulture)}'";
            DateTime actual = JsonConvert.DeserializeObject<DateTime>(expectedUtcString, new EpochDateTimeJsonConverter());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadEpoch()
        {
            long ms = 1469601694391;
            DateTime expected = EpochDateTimeJsonConverter.Epoch.AddMilliseconds(ms);
            var json = $"'{ms}'";
            DateTime actual = JsonConvert.DeserializeObject<DateTime>(json, new EpochDateTimeJsonConverter());
            Assert.Equal(expected, actual);
        }
    }
}
