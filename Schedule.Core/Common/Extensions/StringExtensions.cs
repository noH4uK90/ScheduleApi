namespace Schedule.Core.Common.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return line;
        return char.ToUpper(line[0]) + line[1..].ToLower();
    }

    public static string FirstOrEmpty(this string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return string.Empty;
        return (line.Length == 1 ? line.First() : char.ToUpper(line.First())).ToString();
    }
}