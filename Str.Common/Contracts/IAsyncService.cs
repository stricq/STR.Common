using System;
using System.Threading;
using System.Threading.Tasks;


namespace Str.Common.Contracts {

  public interface IAsyncService {

    Task Run(Action action);

    Task<T> Run<T>(Func<T> function);

    Task Run(Action action, CancellationToken token);

    Task<T> Run<T>(Func<T> function, CancellationToken token);

    Task RunAsync(Action action);

    Task RunAsync(Action action, CancellationToken token);

    Task RunUiContext(Action action);

    Task RunUiContext(Action action, CancellationToken token);

  }

}
