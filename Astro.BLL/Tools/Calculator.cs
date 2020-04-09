using Astro.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Astro.BLL.Tools
{
    public class Calculator
    {
        private readonly ISpeed _speed;
        private readonly ITemperature _temperature;
        private readonly IPressure _pressure;

        public Calculator(ISpeed speed, ITemperature temperature, IPressure pressure)
        {
            _speed = speed;
            _temperature = temperature;
            _pressure = pressure;
        }

        public void Calculate(List<Insight> insights)
        {
            foreach(var item in insights)
            {
                Debug.Print(item.Id.ToString());
                item.AvgPressAT = _pressure.GetAtmospherePressure(Decimal.Parse(item.AvgPress, CultureInfo.InvariantCulture)).ToString();
                item.MaxPressAT = _pressure.GetAtmospherePressure(Decimal.Parse(item.MaxPress, CultureInfo.InvariantCulture)).ToString();
                item.MinPressAT = _pressure.GetAtmospherePressure(Decimal.Parse(item.MinPress, CultureInfo.InvariantCulture)).ToString();
                item.AvgPressBAR = _pressure.GetBarPressure(Decimal.Parse(item.AvgPress, CultureInfo.InvariantCulture)).ToString();
                item.MaxPressBAR = _pressure.GetBarPressure(Decimal.Parse(item.MaxPress, CultureInfo.InvariantCulture)).ToString();
                item.MinPressBAR = _pressure.GetBarPressure(Decimal.Parse(item.MinPress, CultureInfo.InvariantCulture)).ToString();
                item.AvgPressPSI = _pressure.GetPsiPressure(Decimal.Parse(item.AvgPress, CultureInfo.InvariantCulture)).ToString();
                item.MaxPressPSI = _pressure.GetPsiPressure(Decimal.Parse(item.MaxPress, CultureInfo.InvariantCulture)).ToString();
                item.MinPressPSI = _pressure.GetPsiPressure(Decimal.Parse(item.MinPress, CultureInfo.InvariantCulture)).ToString();

                item.AvgTempC = _temperature.GetCelsiusScale(Decimal.Parse(item.AvgTemp, CultureInfo.InvariantCulture)).ToString();
                item.MaxTempC = _temperature.GetCelsiusScale(Decimal.Parse(item.MaxTemp, CultureInfo.InvariantCulture)).ToString();
                item.MinTempC = _temperature.GetCelsiusScale(Decimal.Parse(item.MinTemp, CultureInfo.InvariantCulture)).ToString();
                item.AvgTempK = _temperature.GetKelvinScale(Decimal.Parse(item.AvgTemp, CultureInfo.InvariantCulture)).ToString();
                item.MaxTempK = _temperature.GetKelvinScale(Decimal.Parse(item.MaxTemp, CultureInfo.InvariantCulture)).ToString();
                item.MinTempK = _temperature.GetKelvinScale(Decimal.Parse(item.MinTemp, CultureInfo.InvariantCulture)).ToString();

                item.AvgWindKPH = _speed.GetKiloMeterPerHour(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture)).ToString();
                item.MaxWindKPH = _speed.GetKiloMeterPerHour(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture)).ToString();
                item.MinWindKPH = _speed.GetKiloMeterPerHour(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture)).ToString();
                item.AvgWindMiPH = _speed.GetMilePerHour(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture)).ToString();
                item.MaxWindMiPH = _speed.GetMilePerHour(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture)).ToString();
                item.MinWindMiPH = _speed.GetMilePerHour(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture)).ToString();
                item.AvgWindYard = _speed.GetYardPerMinute(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture)).ToString();
                item.MaxWindYard = _speed.GetYardPerMinute(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture)).ToString();
                item.MinWindYard = _speed.GetYardPerMinute(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture)).ToString();
                item.AvgWindFoot = _speed.GetFootPerMinute(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture)).ToString();
                item.MaxWindFoot = _speed.GetFootPerMinute(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture)).ToString();
                item.MinWindFoot = _speed.GetFootPerMinute(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture)).ToString();
                item.AvgWindKnot = _speed.GetKnot(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture)).ToString();
                item.MaxWindKnot = _speed.GetKnot(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture)).ToString();
                item.MinWindKnot = _speed.GetKnot(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture)).ToString();

                //round
                item.MaxTemp = Decimal.Round(Decimal.Parse(item.MaxTemp, CultureInfo.InvariantCulture), 3).ToString();
                item.AvgTemp = Decimal.Round(Decimal.Parse(item.AvgTemp, CultureInfo.InvariantCulture), 3).ToString();
                item.MinTemp = Decimal.Round(Decimal.Parse(item.MinTemp, CultureInfo.InvariantCulture), 3).ToString();

                item.MaxPress = Decimal.Round(Decimal.Parse(item.MaxPress, CultureInfo.InvariantCulture), 3).ToString();
                item.AvgPress = Decimal.Round(Decimal.Parse(item.AvgPress, CultureInfo.InvariantCulture), 3).ToString();
                item.MinPress = Decimal.Round(Decimal.Parse(item.MinPress, CultureInfo.InvariantCulture), 3).ToString();

                item.MaxWind = Decimal.Round(Decimal.Parse(item.MaxWind, CultureInfo.InvariantCulture), 3).ToString();
                item.AvgWind = Decimal.Round(Decimal.Parse(item.AvgWind, CultureInfo.InvariantCulture), 3).ToString();
                item.MinWind = Decimal.Round(Decimal.Parse(item.MinWind, CultureInfo.InvariantCulture), 3).ToString();
            }
        }
    }
}
