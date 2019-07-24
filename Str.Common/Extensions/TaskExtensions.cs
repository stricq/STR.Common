using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  public static class TaskExtensions {

    #region FireAndForget

    public static void FireAndForget(this Task task) {
      task.ConfigureAwait(false); // Do not mashal back to calling thread.
    }

    public static void FireAndForget(this ValueTask task) {
      task.ConfigureAwait(false);
    }

    #endregion FireAndForget

    #region Fire

    public static ConfiguredTaskAwaitable Fire(this Task task) {
      return task.ConfigureAwait(false);
    }

    public static ConfiguredValueTaskAwaitable Fire(this ValueTask task) {
      return task.ConfigureAwait(false);
    }

    public static ConfiguredTaskAwaitable<T> Fire<T>(this Task<T> task) {
      return task.ConfigureAwait(false);
    }

    public static ConfiguredValueTaskAwaitable<T> Fire<T>(this ValueTask<T> task) {
      return task.ConfigureAwait(false);
    }

    #endregion Fire

  }

}
