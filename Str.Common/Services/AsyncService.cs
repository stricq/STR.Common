using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

using Str.Common.Contracts;


namespace Str.Common.Services {

  [Export(typeof(IAsyncService))]
  public class AsyncService : IAsyncService {

    #region Private Fields

    private readonly TaskScheduler uiContext;
    private readonly TaskScheduler background;

    #endregion Private Fields

    #region Constructor

    public AsyncService() {
      uiContext  = TaskScheduler.FromCurrentSynchronizationContext();
      background = TaskScheduler.Default;
    }

    #endregion Constructor

    #region IAsyncService Implementation

    public Task Run(Action action) {
      return Task.Run(action);
    }

    public Task<T> Run<T>(Func<T> function) {
      return Task.Run(function);
    }

    public Task Run(Action action, CancellationToken token) {
      return Task.Run(action, token);
    }

    public Task<T> Run<T>(Func<T> function, CancellationToken token) {
      return Task.Run(function, token);
    }

    public Task RunAsync(Action action) {
      return Task.Factory.StartNew(action, Task.Factory.CancellationToken, TaskCreationOptions.None, background);
    }

    public Task RunAsync(Action action, CancellationToken token) {
      return Task.Factory.StartNew(action, token, TaskCreationOptions.None, background);
    }

    public Task RunUiContext(Action action) {
      return Task.Factory.StartNew(action, Task.Factory.CancellationToken, TaskCreationOptions.None, uiContext);
    }

    public Task RunUiContext(Action action, CancellationToken token) {
      return Task.Factory.StartNew(action, token, TaskCreationOptions.None, uiContext);
    }

    #endregion IAsyncService Implementation

  }

}
