using Astro.BLL.Tools;
using Xunit;

namespace Astro.Tests
{
    public class DateFormaterTests
    {
        private DateFormater _dateFormater = new DateFormater();

        [Fact]
        public void FormatEPIC_FormatNormalDate()
        {
            string date = "2020-03-12 12:00:00";

            string result = _dateFormater.FormatEPIC(date);

            Assert.Equal("2020/03/12", result);
        }

        [Fact]
        public void FormatEPIC_FormatBadLengthDate()
        {
            string date = "2020-03-1";

            string result = _dateFormater.FormatEPIC(date);

            Assert.Equal("0000/00/00", result);
        }

        [Fact]
        public void FormatEPIC_FormatBadCharDate()
        {
            string date = "2020.03.12";

            string result = _dateFormater.FormatEPIC(date);

            Assert.Equal("0000/00/00", result);
        }
    }
}
