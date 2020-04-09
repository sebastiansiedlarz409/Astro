using System;
using System.Globalization;

namespace Astro.BLL.Tools
{
    public class Temperature : ITemperature
    {
        public decimal GetCelsiusScale(decimal fahrenheitValue)
        {
            return Decimal.Round((fahrenheitValue - 32) / Decimal.Parse("1.8", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetKelvinScale(decimal fahrenheitValue)
        {
            return Decimal.Round(((fahrenheitValue - 32) / Decimal.Parse("1.8", CultureInfo.InvariantCulture)) + Decimal.Parse("273.15", CultureInfo.InvariantCulture), 3);
        }
    }
}
