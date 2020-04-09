namespace Astro.BLL.Tools
{
    public interface ITemperature
    {
        decimal GetKelvinScale(decimal fahrenheitValue);
        decimal GetCelsiusScale(decimal fahrenheitValue);
    }
}
