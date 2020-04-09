using System;
using System.Globalization;

namespace Astro.BLL.Tools
{
    public class Speed : ISpeed
    {
        public decimal GetFootPerMinute(decimal meterPerSecondValue)
        {
            return Decimal.Round(meterPerSecondValue * Decimal.Parse("3.28084", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetKiloMeterPerHour(decimal meterPerSecondValue)
        {
            return Decimal.Round(meterPerSecondValue * Decimal.Parse("3.6", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetKnot(decimal meterPerSecondValue)
        {
            return Decimal.Round(meterPerSecondValue * Decimal.Parse("1.94", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetMilePerHour(decimal meterPerSecondValue)
        {
            return Decimal.Round(meterPerSecondValue * Decimal.Parse("2.23", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetYardPerMinute(decimal meterPerSecondValue)
        {
            return Decimal.Round(meterPerSecondValue * Decimal.Parse("65.6", CultureInfo.InvariantCulture), 3);
        }
    }
}
