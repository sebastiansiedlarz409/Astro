using System;
using System.Collections.Generic;
using System.Text;

namespace Astro.BLL.Tools
{
    interface ISpeed
    {
        double GetKiloMeterPerHour(double meterPerSecondValue);
        double GetMilePerHour(double meterPerSecondValue);
        double GetFootPerMinute(double meterPerSecondValue);
        double GetYardPerMinute(double meterPerSecondValue);

        //można więcej dodać
    }
}
