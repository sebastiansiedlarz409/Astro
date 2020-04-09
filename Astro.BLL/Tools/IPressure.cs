namespace Astro.BLL.Tools
{
    public interface IPressure
    {
        decimal GetBarPressure(decimal paskalValue);
        decimal GetAtmospherePressure(decimal paskalValue);
        decimal GetPsiPressure(decimal paskalValue);
    }
}
