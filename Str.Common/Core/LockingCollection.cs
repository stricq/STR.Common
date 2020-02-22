using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Core {

  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "This is a library.")]
  public class LockingCollection<T> : IList<T>, IList, IReadOnlyList<T> {

    #region Constructors

    public LockingCollection() {
      Items = new LockingList<T>();
    }

    public LockingCollection(IEnumerable<T> enumerable) {
      Items = new LockingList<T>(enumerable);
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

    public IEnumerator<T> GetEnumerator() {
      return Items.GetEnumerator();
    }

    public int IndexOf(T item) {
      return Items.IndexOf(item);
    }

    public void Insert(int index, T item) {
      if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

      if ((uint) index >= (uint) Items.Count) throw new IndexOutOfRangeException();

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

      if ((uint) index >= (uint) Items.Count) throw new IndexOutOfRangeException();

      RemoveItem(index);
    }

    #endregion IList<T> Implementation

    #region ICollection<T> Implementation

    bool ICollection<T>.IsReadOnly => Items.IsReadOnly;

    #endregion ICollection<T> Implementation

    #region ICollection Implementation

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => Items is ICollection coll ? coll.SyncRoot : this;

    void ICollection.CopyTo(Array array, int index) {
      if (array == null) throw new ArgumentNullException(nameof(array));

      if (array.Rank != 1) throw new ArgumentException("Multi-Dimensional arrays are not supported.");

      if (array.GetLowerBound(0) != 0) throw new ArgumentException("Non-Zero lower bound arrays are not supported.");

      if (index < 0) throw new IndexOutOfRangeException("Index is negative.");

      if (array.Length - index < Count) throw new ArgumentException("Array is smaller than collection.");

      if (array is T[] tArray) Items.CopyTo(tArray, index);
      else {
        //
        // Catch the obvious case assignment will fail.
        // We can't find all possible problems by doing the check though.
        // For example, if the element type of the Array is derived from T,
        // we can't figure out if we can successfully copy the element beforehand.
        //
        Type targetType = array.GetType().GetElementType();

        Type sourceType = typeof(T);

        if (!((targetType?.IsAssignableFrom(sourceType) ?? false) || sourceType.IsAssignableFrom(targetType))) {
          throw new ArgumentException("Invalid array type.");
        }
        //
        // We can't cast array of value type to object[], so we don't support
        // widening of primitive types here.
        //
        if (!(array is object[] objects)) {
          throw new ArgumentException("Invalid array type.");
        }

        int count = Items.Count;

        try {
          for(int i = 0; i < count; i++) {
            objects[index++] = Items[i];
          }
        }
        catch(ArrayTypeMismatchException) {
          throw new ArgumentException("Invalid array type.");
        }
      }
    }

    #endregion ICollection Implementation

    #region IEnumerable Implementation

    IEnumerator IEnumerable.GetEnumerator() {
      return (Items as IEnumerable).GetEnumerator();
    }

    #endregion IEnumerable Implementation

    #region IList Implementation

    object IList.this[int index] {
      get => Items[index];
      set {
        try {
          this[index] = (T)value;
        }
        catch(InvalidCastException) {
          throw new ArgumentException("Invalid argument type.");
        }
      }
    }

    bool IList.IsReadOnly => Items.IsReadOnly;

    bool IList.IsFixedSize {
      get {
        //
        // There is no IList<T>.IsFixedSize, so we must assume that only
        // readonly collections are fixed size, if our internal item
        // collection does not implement IList.  Note that Array implements
        // IList, and therefore T[] and U[] will be fixed-size.
        //
        if (Items is IList list) return list.IsFixedSize;

        return Items.IsReadOnly;
      }
    }

    int IList.Add(object value) {
      if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

      try {
        Add((T) value!);
      }
      catch(InvalidCastException) {
        throw new ArgumentException("Invalid argument type.");
      }

      return Count - 1;
    }

    bool IList.Contains(object value) {
      return IsCompatibleObject(value) && Contains((T)value);
    }

    int IList.IndexOf(object value) {
      return IsCompatibleObject(value) ? IndexOf((T)value) : -1;
    }

    void IList.Insert(int index, object value) {
      if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

      try {
        Insert(index, (T)value);
      }
      catch(InvalidCastException) {
        throw new ArgumentException("Invalid argument type.");
      }
    }

    void IList.Remove(object value) {
      if (Items.IsReadOnly) throw new NotSupportedException("Collection is read only.");

      if (IsCompatibleObject(value)) Remove((T)value);
    }

    #endregion IList Implementation

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

    #region Private Methods

    private static bool IsCompatibleObject(object value) {
      return ((value is T) || (value == null && default(T) == null));
    }

    #endregion Private Methods

  }

}
