using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

using Str.Common.Core;


namespace Str.Common.Extensions;


[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
public static class ObservableCollectionExtensions {

  #region AddRange

  public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> Items) {
    foreach(T item in Items) collection.Add(item);
  }

  public static void AddRange<T>(this LockingObservableCollection<T> collection, IEnumerable<T> Items) {
    foreach(T item in Items) collection.Add(item);
  }

  #endregion AddRange

  #region Ordered Merge

  public static void OrderedMerge<T>(this ObservableCollection<T> list, T item) where T : IComparable<T> {
    list.OrderedMerge(new List<T> { item });
  }

  public static void OrderedMerge<T>(this ObservableCollection<T> list, IEnumerable<T> items) where T : IComparable<T> {
    List<T> itemList = items.ToList();

    if (!itemList.Any()) return;

    if (!list.Any()) {
      list.AddRange(itemList);

      return;
    }

    for(int i = 0; i < list.Count; ++i) {
      if (itemList.Count == 0) break;

      if (itemList[0].CompareTo(list[i]) >= 0) continue;

      list.Insert(i, itemList[0]);

      itemList.RemoveAt(0);
    }

    if (itemList.Count > 0) list.AddRange(itemList);
  }

  public static void OrderedMerge<T>(this ObservableCollection<T> list, T item, Func<T, T, int> comparer) {
    list.OrderedMerge(new List<T> { item }, comparer);
  }

  public static void OrderedMerge<T>(this ObservableCollection<T> list, IEnumerable<T> items, Func<T, T, int> comparer) {
    List<T> itemList = items.ToList();

    if (!itemList.Any()) return;

    if (!list.Any()) {
      list.AddRange(itemList);

      return;
    }

    for(int i = 0; i < list.Count; ++i) {
      if (itemList.Count == 0) break;

      if (comparer(itemList[0], list[i]) >= 0) continue;

      list.Insert(i, itemList[0]);

      itemList.RemoveAt(0);
    }

    if (itemList.Count > 0) list.AddRange(itemList);
  }

  public static void OrderedMerge<T>(this LockingObservableCollection<T> list, T item) where T : IComparable<T> {
    list.OrderedMerge(new List<T> {item});
  }

  public static void OrderedMerge<T>(this LockingObservableCollection<T> list, IEnumerable<T> items) where T : IComparable<T> {
    List<T> itemList = items.ToList();

    if (!itemList.Any()) return;

    if (!list.Any()) {
      list.AddRange(itemList);

      return;
    }

    for(int i = 0; i < list.Count; ++i) {
      if (itemList.Count == 0) break;

      if (itemList[0].CompareTo(list[i]) >= 0) continue;

      list.Insert(i, itemList[0]);

      itemList.RemoveAt(0);
    }

    if (itemList.Count > 0) list.AddRange(itemList);
  }

  public static void OrderedMerge<T>(this LockingObservableCollection<T> list, T item, Func<T, T, int> comparer) {
    list.OrderedMerge(new List<T> {item}, comparer);
  }

  public static void OrderedMerge<T>(this LockingObservableCollection<T> list, IEnumerable<T> items, Func<T, T, int> comparer) {
    List<T> itemList = items.ToList();

    if (!itemList.Any()) return;

    if (!list.Any()) {
      list.AddRange(itemList);

      return;
    }

    for(int i = 0; i < list.Count; ++i) {
      if (itemList.Count == 0) break;

      if (comparer(itemList[0], list[i]) >= 0) continue;

      list.Insert(i, itemList[0]);

      itemList.RemoveAt(0);
    }

    if (itemList.Count > 0) list.AddRange(itemList);
  }

  #endregion Ordered Merge

  #region Sort

  public static void Sort<T>(this ObservableCollection<T> list, IComparer<T> comparer) {
    List<T> sorted = list.OrderBy(item => item, comparer).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  public static void Sort<TSource, TKey>(this ObservableCollection<TSource> list, Func<TSource, TKey> comparer) {
    List<TSource> sorted = list.OrderBy(comparer).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  public static void Sort<T>(this ObservableCollection<T> list) where T : IComparable<T> {
    List<T> sorted = list.OrderBy(item => item).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  public static void Sort<T>(this LockingObservableCollection<T> list, IComparer<T> comparer) {
    List<T> sorted = list.OrderBy(item => item, comparer).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  public static void Sort<TSource, TKey>(this LockingObservableCollection<TSource> list, Func<TSource, TKey> comparer) {
    List<TSource> sorted = list.OrderBy(comparer).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  public static void Sort<T>(this LockingObservableCollection<T> list) where T : IComparable<T> {
    List<T> sorted = list.OrderBy(item => item).ToList();

    sorted.ForEach(item => list.Move(list.IndexOf(item), sorted.IndexOf(item)));
  }

  #endregion Sort

}
