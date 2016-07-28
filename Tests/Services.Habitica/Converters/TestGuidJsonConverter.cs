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
    public class TestGuidJsonConverter
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
            var guid = Guid.NewGuid();
            string actual = JsonConvert.SerializeObject(guid.ToString(), new GuidJsonConverter());
            Assert.Equal(guid.ToString(), actual);
        }

        [Fact]
        public void WriteGuidToJson()
        {
            var guid = Guid.NewGuid();
            string actual = JsonConvert.SerializeObject(guid, new GuidJsonConverter());
            Assert.Equal(guid.ToString(), actual);
        }

        [Fact]
        public void ReadJson()
        {
            var expected = Guid.NewGuid();
            string s = $"'{expected.ToString()}'";
            Guid actual = JsonConvert.DeserializeObject<Guid>(s, new GuidJsonConverter());
            Assert.Equal(expected, actual);
        }
    }
}
