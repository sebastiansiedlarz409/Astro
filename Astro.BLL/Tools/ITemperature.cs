namespace Astro.BLL.Tools
{
    interface ITemperature
    {
        double GetKelvinScale(double celsiusValue);
        double GetFahrenheitScale(double celsiusValue);
    }
}
