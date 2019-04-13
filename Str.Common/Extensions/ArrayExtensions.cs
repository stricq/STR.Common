using System;

using Str.Common.Helpers;


namespace Str.Common.Extensions {

  public static class ArrayExtensions {

    public static void ForEach(this Array Array, Action<Array, int[]> Action) {
      if (Array.LongLength == 0) return;

      ArrayTraverse walker = new ArrayTraverse(Array);

      do Action(Array, walker.Position);
      while(walker.Step());
    }

  }

}
