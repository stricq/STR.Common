using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  public static class TaskExtensions {

    #region FireAndForget

    public static void FireAndForget(this Task task, Action<Exception> onException = null) {
      try {
        Task.Run(() => task).ConfigureAwait(false);
      }
      catch(Exception ex) when(onException != null) {
        onException(ex);
      }
    }

    public static void FireAndForget<T>(this Task task, T context, Action<T, Exception> onException) {
      try {
        Task.Run(() => task).ConfigureAwait(false);
      }
      catch(Exception ex) {
        onException(context, ex);
      }
    }

    public static void FireAndForget(this ValueTask task, Action<Exception> onException = null) {
      try {
        Task.Run(() => task).ConfigureAwait(false);
      }
      catch(Exception ex) when(onException != null) {
        onException(ex);
      }
    }

    public static void FireAndForget<T>(this ValueTask task, T context, Action<T, Exception> onException) {
      try {
        Task.Run(() => task).ConfigureAwait(false);
      }
      catch(Exception ex) {
        onException(context, ex);
      }
    }

    #endregion FireAndForget

    #region FireAndWait

    public static void FireAndWait(this Task task, bool continueOnCapturedContext = false) {
      task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    public static void FireAndWait(this ValueTask task, bool continueOnCapturedContext = false) {
      task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    public static T FireAndWait<T>(this Task<T> task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    public static T FireAndWait<T>(this ValueTask<T> task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    #endregion FireAndWait

    #region Fire

    public static ConfiguredTaskAwaitable Fire(this Task task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext);
    }

    public static ConfiguredValueTaskAwaitable Fire(this ValueTask task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext);
    }

    public static ConfiguredTaskAwaitable<T> Fire<T>(this Task<T> task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext);
    }

    public static ConfiguredValueTaskAwaitable<T> Fire<T>(this ValueTask<T> task, bool continueOnCapturedContext = false) {
      return task.ConfigureAwait(continueOnCapturedContext);
    }

    #endregion Fire

  }

}
