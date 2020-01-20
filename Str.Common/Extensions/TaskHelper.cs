using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
  public static class TaskHelper {

    #region Private Fields

    private static TaskScheduler uiScheduler;

    #endregion Private Fields

    #region Static Public Methods

    public static void InitializeOnUiThread() {
      uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }

    public static Task RunOnUiThreadAsync(Action action) {
      return RunOnUiThreadAsync(action, CancellationToken.None);
    }

    public static Task RunOnUiThreadAsync(Action action, CancellationToken token) {
      return Task.Factory.StartNew(action, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task RunOnUiThreadAsync(Func<Task> func) {
      return RunOnUiThreadAsync(func, CancellationToken.None);
    }

    public static Task RunOnUiThreadAsync(Func<Task> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task<TResult> RunOnUiThreadAsync<TResult>(Func<TResult> func) {
      return RunOnUiThreadAsync(func, CancellationToken.None);
    }

    public static Task<TResult> RunOnUiThreadAsync<TResult>(Func<TResult> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task<TResult> RunOnUiThreadAsync<TResult>(Func<Task<TResult>> func) {
      return RunOnUiThreadAsync(func, CancellationToken.None);
    }

    public static Task<TResult> RunOnUiThreadAsync<TResult>(Func<Task<TResult>> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler).Unwrap();
    }

    #endregion Static Public Methods

  }

}