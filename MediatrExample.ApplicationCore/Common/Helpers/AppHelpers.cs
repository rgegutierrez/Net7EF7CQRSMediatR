using HashidsNet;
using System.Globalization;

namespace MediatrExample.ApplicationCore.Common.Helpers;

public static class AppHelpers
{
    public const string HashIdsSalt = "s3cret_s4lt";

    public static string FromDotToComma(this decimal number) =>
        number.ToString("0.##", CultureInfo.GetCultureInfo("es-CL"));

    public static string FromDotToComma(this decimal? number) {
        if (number == null) return "";
        var aux = (decimal)number;
        return aux.ToString("0.##", CultureInfo.GetCultureInfo("es-CL"));
    }
        

    public static string ToHashId(this int number) =>
        GetHasher().Encode(number);

    public static int FromHashId(this string encoded) =>
        GetHasher().Decode(encoded).FirstOrDefault();

    private static Hashids GetHasher() => new(HashIdsSalt, 8);

    public static string DatetimeToString(this DateTime? dt, string format) => 
        dt == null ? null : ((DateTime)dt).ToString(format);
}
