using System;


namespace Str.Common.Helpers {

  internal class ArrayTraverse {

    #region Private Fields

    private readonly int[] maxLengths;

    #endregion Private Fields

    #region Constructor

    public ArrayTraverse(Array array) {
      maxLengths = new int[array.Rank];

      for(int i = 0; i < array.Rank; ++i) {
        maxLengths[i] = array.GetLength(i) - 1;
      }

      Position = new int[array.Rank];
    }

    #endregion Constructor

    #region Properties

    public int[] Position { get; }

    #endregion Properties

    #region Public Methods

    public bool Step() {
      for(int i = 0; i < Position.Length; ++i) {
        if (Position[i] >= maxLengths[i]) continue;

        Position[i]++;

        for(int j = 0; j < i; j++) Position[j] = 0;

        return true;
      }

      return false;
    }

    #endregion Public Methods

  }

}
