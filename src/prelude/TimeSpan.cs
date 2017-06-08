using System;

public static class TimeSpanExtensions
{
    public static TimeSpan milliseconds(this int milliseconds) => TimeSpan.FromMilliseconds(milliseconds);    
    public static TimeSpan seconds(this int seconds) => TimeSpan.FromSeconds(seconds);
    public static TimeSpan minutes(this int minutes) => TimeSpan.FromMinutes(minutes);
    public static TimeSpan hours(this int hours) => TimeSpan.FromHours(hours);
    public static TimeSpan days(this int days) => TimeSpan.FromDays(days);        
}