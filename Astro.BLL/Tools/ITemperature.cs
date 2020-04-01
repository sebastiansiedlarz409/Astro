namespace Astro.BLL.Tools
{
    interface ITemperature
    {
        double GetKelvinScale(double fahrenheitValue);
        double GetCelsiusScale(double fahrenheitValue);
    }
}
