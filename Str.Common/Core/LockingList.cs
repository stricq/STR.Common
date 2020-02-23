using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;


namespace Str.Common.Core {

  // https://codereview.stackexchange.com/questions/7276/reader-writer-collection

  [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "This is a library.")]
  public class LockingList<T> : IList<T> {

    #region Private Fields

    private readonly List<T> inner;

    private readonly ReaderWriterLockSlim innerLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

    #endregion Private Fields

    #region Constructors

    public LockingList() {
      inner = new List<T>();
    }

    public LockingList(int capacity) {
      inner = new List<T>(capacity);
    }

    public LockingList(IEnumerable<T> enumerable) {
      inner = enumerable == null ? new List<T>() : new List<T>(enumerable);
    }

    #endregion Constructors

    #region IList<T> Implementation

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
      innerLock.EnterReadLock();

      try {
        return new LockingEnumerator<T>(inner.GetEnumerator(), innerLock);
      }
      finally {
        innerLock.ExitReadLock();
      }
    }

    public IEnumerator GetEnumerator() {
      return (this as IEnumerable<T>).GetEnumerator();
    }

    public void Add(T item) {
      innerLock.EnterWriteLock();

      try {
        inner.Add(item);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public void Clear() {
      innerLock.EnterWriteLock();

      try {
        inner.Clear();
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public bool Contains(T item) {
      innerLock.EnterReadLock();

      try {
        return inner.Contains(item);
      }
      finally {
        innerLock.ExitReadLock();
      }
    }

    public void CopyTo(T[] array, int arrayIndex) {
      innerLock.EnterReadLock();

      try {
        inner.CopyTo(array, arrayIndex);
      }
      finally {
        innerLock.ExitReadLock();
      }
    }

    public bool Remove(T item) {
      innerLock.EnterWriteLock();

      try {
        return inner.Remove(item);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public int Count {
      get {
        innerLock.EnterReadLock();

        try {
          return inner.Count;
        }
        finally {
          innerLock.ExitReadLock();
        }
      }
    }

    public bool IsReadOnly {
      get {
        innerLock.EnterReadLock();

        try {
          return (inner as ICollection<T>).IsReadOnly;
        }
        finally {
          innerLock.ExitReadLock();
        }
      }
    }

    public int IndexOf(T item) {
      innerLock.EnterReadLock();

      try {
        return inner.IndexOf(item);
      }
      finally {
        innerLock.ExitReadLock();
      }
    }

    public void Insert(int index, T item) {
      innerLock.EnterWriteLock();

      try {
        inner.Insert(index, item);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public void RemoveAt(int index) {
      innerLock.EnterWriteLock();

      try {
        inner.RemoveAt(index);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public T this[int index] {
      get {
        innerLock.EnterReadLock();

        try {
          return inner[index];
        }
        finally {
          innerLock.ExitReadLock();
        }
      }
      set {
        innerLock.EnterWriteLock();

        try {
          inner[index] = value;
        }
        finally {
          innerLock.ExitWriteLock();
        }
      }
    }

    #endregion IList<T> Implementation

    #region Public Methods

    public void AddRange(IEnumerable<T> collection) {
      innerLock.EnterWriteLock();

      try {
        inner.AddRange(collection);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    public bool Exists(Predicate<T> match) {
      innerLock.EnterReadLock();

      try {
        return inner.Exists(match);
      }
      finally {
        innerLock.ExitReadLock();
      }
    }

    public int RemoveAll(Predicate<T> match) {
      innerLock.EnterWriteLock();

      try {
        return inner.RemoveAll(match);
      }
      finally {
        innerLock.ExitWriteLock();
      }
    }

    #endregion Public Methods

  }

}
