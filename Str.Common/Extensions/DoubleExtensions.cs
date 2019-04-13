using System;


namespace Str.Common.Extensions {

  public static class DoubleExtensions {
    //
    // This method comes from an answer on Stack Overflow
    //
    // https://stackoverflow.com/a/37403737/1135296
    //
    public static bool EqualInPercentRange(this double Value1, double Value2, long Units = 2) {
      long longValue1 = BitConverter.DoubleToInt64Bits(Value1);
      long longValue2 = BitConverter.DoubleToInt64Bits(Value2);
      //
      // If the signs are different, return false except for +0 and -0.
      //
      if (longValue1 >> 63 != longValue2 >> 63) {
        //
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        //
        return Value1 == Value2;
      }

      long diff = Math.Abs(longValue1 - longValue2);

      return diff <= Units;
    }

  }

}
