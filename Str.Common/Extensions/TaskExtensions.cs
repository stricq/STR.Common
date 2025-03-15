using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


namespace Str.Common.Extensions;


[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "InconsistentNaming",  Justification = "No.")]
public static class TaskExtensions {

    #region FireAndForget

#pragma warning disable IDE1006 // Naming Styles

    [SuppressMessage("AsyncUsage", "AsyncFixer03:Fire-and-forget async-void methods or delegates", Justification = "Exceptions are handled.")]
    public static async void FireAndForget(this Task task, Action<Exception>? onException = null) {
        try {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex) {
            onException?.Invoke(ex);
        }
    }

    [SuppressMessage("AsyncUsage", "AsyncFixer03:Fire-and-forget async-void methods or delegates", Justification = "Exceptions are handled.")]
    public static async void FireAndForget<T>(this Task task, T context, Action<T, Exception> onException) {
        try {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex) {
            onException(context, ex);
        }
    }

    [SuppressMessage("AsyncUsage", "AsyncFixer03:Fire-and-forget async-void methods or delegates", Justification = "Exceptions are handled.")]
    public static async void FireAndForget(this ValueTask task, Action<Exception>? onException = null) {
        try {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex) {
            onException?.Invoke(ex);
        }
    }

    [SuppressMessage("AsyncUsage", "AsyncFixer03:Fire-and-forget async-void methods or delegates", Justification = "Exceptions are handled.")]
    public static async void FireAndForget<T>(this ValueTask task, T context, Action<T, Exception> onException) {
        try {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex) {
            onException(context, ex);
        }
    }

#pragma warning restore IDE1006 // Naming Styles

    #endregion FireAndForget

    #region FireAndWait

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void FireAndWait(this Task task, bool continueOnCapturedContext = false) {
        task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void FireAndWait(this ValueTask task, bool continueOnCapturedContext = false) {
        task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T FireAndWait<T>(this Task<T> task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T FireAndWait<T>(this ValueTask<T> task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    #endregion FireAndWait

    #region Fire

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredTaskAwaitable Fire(this Task task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredValueTaskAwaitable Fire(this ValueTask task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredTaskAwaitable<T> Fire<T>(this Task<T> task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredValueTaskAwaitable<T> Fire<T>(this ValueTask<T> task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConfiguredCancelableAsyncEnumerable<T> Fire<T>(this IAsyncEnumerable<T> task, bool continueOnCapturedContext = false) {
        return task.ConfigureAwait(continueOnCapturedContext);
    }

    #endregion Fire

}
