namespace Astro.BLL.Tools
{
    public interface ISpeed
    {
        decimal GetKiloMeterPerHour(decimal meterPerSecondValue);
        decimal GetMilePerHour(decimal meterPerSecondValue);
        decimal GetFootPerMinute(decimal meterPerSecondValue);
        decimal GetYardPerMinute(decimal meterPerSecondValue);
        decimal GetKnot(decimal meterPerSecondValue);
    }
}
