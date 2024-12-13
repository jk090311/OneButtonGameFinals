using System;

public class TimestampConverter
{
    public static DateTime ConvertTimestamp(long timestamp)
    {
        // Unix epoch starts at January 1, 1970 (UTC)
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Add the seconds to the epoch to get the DateTime
        return epoch.AddSeconds(timestamp).ToLocalTime(); // Convert to local time
    }
}