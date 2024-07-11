using System.Collections;

using JetBrains.Annotations;


namespace Str.Common.Core;


public sealed class LockingReadOnlyCollection<T> : IList<T>, IReadOnlyList<T> {

    #region Private Fields

    private readonly LockingList<T> list;

    private readonly NotSupportedException notSupportedException;

    #endregion Private Fields

    #region Constructor

    public LockingReadOnlyCollection(IEnumerable<T> list) {
        this.list = new LockingList<T>(list);

        notSupportedException = new NotSupportedException("This is a Read Only Collection.");
    }

    #endregion Constructor

    #region ICollection<T> Implementation

    public int Count => list.Count;

    public bool Contains(T value) {
        return list.Contains(value);
    }

    public void CopyTo(T[] array, int index) {
        list.CopyTo(array, index);
    }

    public bool IsReadOnly => true;

    void ICollection<T>.Add(T value) {
        throw notSupportedException;
    }

    void ICollection<T>.Clear() {
        throw notSupportedException;
    }

    bool ICollection<T>.Remove(T value) {
        throw notSupportedException;
    }

    #endregion ICollection<T> Implementation

    #region IEnumerable<T> Implementation

    [MustDisposeResource]
    public IEnumerator<T> GetEnumerator() {
        return ((IList<T>)list).GetEnumerator();
    }

    [MustDisposeResource]
    IEnumerator IEnumerable.GetEnumerator() {
        return list.GetEnumerator();
    }

    #endregion IEnumerable<T> Implementation

    #region IList<T> Implementation

    public int IndexOf(T value) {
        return list.IndexOf(value);
    }

    T IList<T>.this[int index] {
        get => list[index];
        set => throw notSupportedException;
    }

    void IList<T>.Insert(int index, T value) {
        throw notSupportedException;
    }

    void IList<T>.RemoveAt(int index) {
        throw notSupportedException;
    }

    #endregion IList<T> Implementation

    #region IReadOnlyList<T> Implementation

    public T this[int index] => list[index];

    #endregion IReadOnlyList<T> Implementation

}