using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Extensions; 

[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedType.Global",   Justification = "This is a library.")]
public static class DoubleExtensions {
  //
  // https://docs.microsoft.com/en-us/dotnet/api/system.double.equals
  //
  public static bool HasMinimalDifference(this double Value1, double Value2, long Units = 2) {
    long longValue1 = BitConverter.DoubleToInt64Bits(Value1);
    long longValue2 = BitConverter.DoubleToInt64Bits(Value2);
    //
    // If the signs are different, return false except for +0 and -0.
    //
    if (longValue1 >> 63 != longValue2 >> 63) {
      return Value1 == 0d && Value2 == 0d;
    }

    long diff = Math.Abs(longValue1 - longValue2);

    return diff <= Units;
  }

}