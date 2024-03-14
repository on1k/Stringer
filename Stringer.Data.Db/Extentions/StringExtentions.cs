namespace Stringer.Data.Db.Extentions;

public static class StringExtentions
{
    public static bool IsValidLength(this string text) => text.Length < 3000;
}