using System.Collections;
using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;


namespace Str.Common.Core;


[SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "This is a library.")]
public class LockingCollection<T> : IList<T>, IReadOnlyList<T> {

    #region Constructors

    public LockingCollection() {
        Items = [];
    }

    public LockingCollection(int capacity) {
        Items = new LockingList<T>(capacity);
    }

    public LockingCollection(IEnumerable<T> enumerable) {
        Items = [.. enumerable];
    }

    #endregion Constructors

    #region IList<T> Implementation

    public T this[int index] {
        get => Items[index];
        set {
            if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

            if ((uint)index >= (uint)Items.Count) throw new IndexOutOfRangeException();

            SetItem(index, value);
        }
    }

    public int Count => Items.Count;

    public void Add(T item) {
        if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

        int index = Items.Count;

        InsertItem(index, item);
    }

    public void Clear() {
        if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

        ClearItems();
    }

    public void CopyTo(T[] array, int index) {
        Items.CopyTo(array, index);
    }

    public bool Contains(T item) {
        return Items.Contains(item);
    }

    [MustDisposeResource]
    public IEnumerator<T> GetEnumerator() {
        return Items.GetEnumerator();
    }

    public int IndexOf(T item) {
        return Items.IndexOf(item);
    }

    public void Insert(int index, T item) {
        if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

        if ((uint)index >= (uint)Items.Count) throw new IndexOutOfRangeException();

        InsertItem(index, item);
    }

    public bool Remove(T item) {
        if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

        int index = Items.IndexOf(item);

        if (index < 0) return false;

        RemoveItem(index);

        return true;
    }

    public void RemoveAt(int index) {
        if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

        if ((uint)index >= (uint)Items.Count) throw new IndexOutOfRangeException();

        RemoveItem(index);
    }

    #endregion IList<T> Implementation

    #region ICollection<T> Implementation

    bool ICollection<T>.IsReadOnly => Items.IsReadOnly;

    #endregion ICollection<T> Implementation

    #region IEnumerable Implementation

    [MustDisposeResource]
    IEnumerator IEnumerable.GetEnumerator() {
        return (Items as IEnumerable).GetEnumerator();
    }

    #endregion IEnumerable Implementation

    #region Protected Methods

    protected IList<T> Items { get; }

    protected virtual void SetItem(int index, T item) {
        Items[index] = item;
    }

    protected virtual void RemoveItem(int index) {
        Items.RemoveAt(index);
    }

    protected virtual void InsertItem(int index, T item) {
        Items.Insert(index, item);
    }

    protected virtual void ClearItems() {
        Items.Clear();
    }

    #endregion Protected Methods

}
