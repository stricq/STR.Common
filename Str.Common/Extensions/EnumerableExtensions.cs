using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using Str.Common.Contracts;


namespace Str.Common.Extensions {

  public static class EnumerableExtensions {

    #region ForEach

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      foreach(T item in source) action(item);
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
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : ITraversable<T> {
      List<T> list = new List<T>();

      Y<IEnumerable<T>>(f => items => {
        if(items == null) return;

        list.AddRange(items.Where(predicate));

        foreach(T i in items) f(i.Children);
      })(source);

      return list;
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source) where T : ITraversable<T> {
      List<T> list = new List<T>();

      Y<IEnumerable<T>>(f => items => {
        if(items == null) return;

        list.AddRange(items);

        foreach(T i in items) f(i.Children);
      })(source);

      return list;
    }

    private static Action<A> Y<A>(Func<Action<A>, Action<A>> F) {
      return a => F(Y(F))(a);
    }

    #endregion Traverse

  }

}
