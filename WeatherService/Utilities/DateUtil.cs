namespace WeatherService.Utilities;

public class DateUtil
{
    public string GetCurrentDateFormatted()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }
}