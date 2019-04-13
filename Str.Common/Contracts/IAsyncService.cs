using System;
using System.Threading;
using System.Threading.Tasks;


namespace Str.Common.Contracts {

  public interface IAsyncService {

    Task Run(Action Action);

    Task<T> Run<T>(Func<T> Function);

    Task Run(Action Action, CancellationToken Token);

    Task<T> Run<T>(Func<T> Function, CancellationToken Token);

    Task RunAsync(Action Action);

    Task RunAsync(Action Action, CancellationToken Token);

    Task RunUiContext(Action Action);

    Task RunUiContext(Action Action, CancellationToken Token);

  }

}
