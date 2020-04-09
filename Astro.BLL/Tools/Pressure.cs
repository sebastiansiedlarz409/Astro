using System;
using System.Globalization;

namespace Astro.BLL.Tools
{
    public class Pressure : IPressure
    {
        public decimal GetAtmospherePressure(decimal paskalValue)
        {
            return Decimal.Round(paskalValue * Decimal.Parse("0.000009", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetBarPressure(decimal paskalValue)
        {
            return Decimal.Round(paskalValue * Decimal.Parse("0.00001", CultureInfo.InvariantCulture), 3);
        }

        public decimal GetPsiPressure(decimal paskalValue)
        {
            return Decimal.Round(paskalValue * Decimal.Parse("0.00014", CultureInfo.InvariantCulture), 3);
        }
    }
}
