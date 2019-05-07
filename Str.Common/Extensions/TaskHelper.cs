using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
  public static class TaskHelper {

    private static readonly TaskScheduler uiScheduler;

    static TaskHelper() {
      uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }

    public static Task RunOnUiThread(Action action) {
      return RunOnUiThread(action, CancellationToken.None);
    }

    public static Task RunOnUiThread(Action action, CancellationToken token) {
      return Task.Factory.StartNew(action, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task RunOnUiThread(Func<Task> func) {
      return RunOnUiThread(func, CancellationToken.None);
    }

    public static Task RunOnUiThread(Func<Task> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task<TResult> RunOnUiThread<TResult>(Func<TResult> func) {
      return RunOnUiThread(func, CancellationToken.None);
    }

    public static Task<TResult> RunOnUiThread<TResult>(Func<TResult> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler);
    }

    public static Task<TResult> RunOnUiThread<TResult>(Func<Task<TResult>> func) {
      return RunOnUiThread(func, CancellationToken.None);
    }

    public static Task<TResult> RunOnUiThread<TResult>(Func<Task<TResult>> func, CancellationToken token) {
      return Task.Factory.StartNew(func, token, TaskCreationOptions.DenyChildAttach, uiScheduler).Result;
    }

  }

}
