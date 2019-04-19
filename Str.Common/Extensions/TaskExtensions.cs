using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  public static class TaskExtensions {

    #region FireAndForget

    public static void FireAndForget(this Task Task) {
      Task.ConfigureAwait(false); // Do not mashal back to calling thread.
    }

    #endregion FireAndForget

    #region Fire

    public static ConfiguredTaskAwaitable Fire(this Task task) {
      return task.ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<T> Fire<T>(this Task<T> task) {
      return task.ConfigureAwait(false);
    }

    #endregion Fire

  }

}
