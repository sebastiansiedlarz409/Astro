using System;
using System.Collections.Generic;
using System.Text;

namespace Astro.BLL.Tools
{
    interface IPressure
    {
        double GetBarPressure(double paskalValue);
        double GetAtmospherePressure(double paskalValue);
        double GetPsiPressure(double paskalValue);
    }
}
