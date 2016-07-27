using System;
using System.Collections.Generic;
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
            Assert.True(new GuidConverter().CanConvert(typeof(string)));
        }

        [Fact]
        public void CanConvertGuid()
        {
            Assert.True(new GuidConverter().CanConvert(typeof(Guid)));
        }

        [Fact]
        public void StringToJson()
        {
            Assert.True(false, "incomplete");
        }

        [Fact]
        public void GuidToJson()
        {
            Assert.True(false, "incomplete");
        }

        [Fact]
        public void JsonToString()
        {
            Assert.True(false, "incomplete");
        }

        [Fact]
        public void JsonToGuid()
        {
            Assert.True(false, "incomplete");
        }
    }
}
