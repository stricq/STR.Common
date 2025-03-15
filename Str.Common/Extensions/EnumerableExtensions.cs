using System.Diagnostics.CodeAnalysis;

using Str.Common.Contracts;
using Str.Common.Core;


namespace Str.Common.Extensions;


[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedType.Global",   Justification = "This is a library.")]
public static class EnumerableExtensions {

    #region ForEach

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
        foreach (T item in source) action(item);
    }

    #endregion ForEach

    #region ForEachAsync

    public static Task ForEachAsync<TIn>(this IEnumerable<TIn> source, Func<TIn, Task> function) {
        return Task.WhenAll(source.AsParallel().Select(function));
    }

    public static Task<TOut[]> ForEachAsync<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, Task<TOut>> function) {
        return Task.WhenAll(source.AsParallel().Select(function));
    }

    #endregion ForEachAsync

    #region Traverse
    //
    // The Traverse method comes from Stack Overflow but I can no longer find the question
    //
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "This is a library.")]
    public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : ITraversable<T> {
        List<T> list = [];

        Y<IEnumerable<T>>(f => items => {
            list.AddRange(items.Where(predicate));

            foreach (T i in items) f(i.Children);
        })(source);

        return list;
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "This is a library.")]
    public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source) where T : ITraversable<T> {
        List<T> list = [];

        Y<IEnumerable<T>>(f => items => {
            list.AddRange(items);

            foreach (T i in items) f(i.Children);
        })(source);

        return list;
    }

    private static Action<TA> Y<TA>(Func<Action<TA>, Action<TA>> F) {
        return a => F(Y(F))(a);
    }

    #endregion Traverse

    #region ToLocking...

    public static LockingList<T> ToLockingList<T>(this IEnumerable<T> enumerable) {
        return [.. enumerable];
    }

    public static LockingCollection<T> ToLockingCollection<T>(this IEnumerable<T> enumerable) {
        return [.. enumerable];
    }

    public static LockingObservableCollection<T> ToLockingObservableCollection<T>(this IEnumerable<T> enumerable) {
        return [.. enumerable];
    }

    public static LockingReadOnlyCollection<T> ToLockingReadOnlyCollection<T>(this IEnumerable<T> enumerable) {
        return new LockingReadOnlyCollection<T>(enumerable);
    }

    #endregion ToLocking...

}
