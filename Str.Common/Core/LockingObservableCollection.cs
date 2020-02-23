using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Core {

  [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "This is a library.")]
  public class LockingObservableCollection<T> : LockingCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged {

    #region Private Fields

    private int blockReentrancyCount;

    private SimpleMonitor monitor;

    #endregion Private Fields

    #region Constructors

    public LockingObservableCollection() { }

    public LockingObservableCollection(int capacity) : base(capacity) { }

    public LockingObservableCollection(IEnumerable<T> enumerable) : base(enumerable) { }

    #endregion Constructors

    #region INotifyCollectionChanged Implementation

    public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

    #endregion INotifyCollectionChanged Implementation

    #region INotifyPropertyChanged Implementation

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
      add    => PropertyChanged += value;
      remove => PropertyChanged -= value;
    }

    protected virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion INotifyPropertyChanged Implementation

    #region Public Methods

    public void Move(int oldIndex, int newIndex) => MoveItem(oldIndex, newIndex);

    public virtual void MoveItem(int oldIndex, int newIndex) {
      CheckReentrancy();

      T removedItem = this[oldIndex];

      base.RemoveItem(oldIndex);

      base.InsertItem(newIndex, removedItem);

      OnIndexerPropertyChanged();

      OnCollectionChanged(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex);
    }

    #endregion Public Methods

    #region LockingCollection<T> Overrides

    protected override void ClearItems() {
      CheckReentrancy();

      base.ClearItems();

      OnCountPropertyChanged();

      OnIndexerPropertyChanged();

      OnCollectionReset();
    }

    protected override void RemoveItem(int index) {
      CheckReentrancy();

      T removedItem = this[index];

      base.RemoveItem(index);

      OnCountPropertyChanged();

      OnIndexerPropertyChanged();

      OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);
    }

    protected override void InsertItem(int index, T item) {
      CheckReentrancy();

      base.InsertItem(index, item);

      OnCountPropertyChanged();

      OnIndexerPropertyChanged();

      OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
    }

    protected override void SetItem(int index, T item) {
      CheckReentrancy();

      T originalItem = this[index];

      base.SetItem(index, item);

      OnIndexerPropertyChanged();

      OnCollectionChanged(NotifyCollectionChangedAction.Replace, originalItem, item, index);
    }

    #endregion LockingCollection<T> Overrides

    #region Protected Methods

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
      PropertyChanged?.Invoke(this, e);
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
      NotifyCollectionChangedEventHandler handler = CollectionChanged;

      if (handler == null) return;
      //
      // Not calling BlockReentrancy() here to avoid the SimpleMonitor allocation.
      //
      blockReentrancyCount++;

      try {
        handler(this, e);
      }
      finally {
        blockReentrancyCount--;
      }
    }

    protected void CheckReentrancy() {
      if (blockReentrancyCount <= 0) return;
      //
      // we can allow changes if there's only one listener - the problem
      // only arises if reentrant changes make the original event args
      // invalid for later listeners.  This keeps existing code working
      // (e.g. Selector.SelectedItems).
      //
      if (CollectionChanged?.GetInvocationList().Length > 1) throw new InvalidOperationException("LockingObservableCollection does not allow re-entrancy.");
    }

    protected IDisposable BlockReentrancy() {
      blockReentrancyCount++;

      return EnsureMonitorInitialized();
    }

    #endregion Protected Methods

    #region Private Methods

    private void OnCountPropertyChanged() => OnPropertyChanged(EventArgsCache.CountPropertyChanged);

    private void OnIndexerPropertyChanged() => OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index) {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex) {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index) {
      OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
    }

    private void OnCollectionReset() => OnCollectionChanged(EventArgsCache.ResetCollectionChanged);

    private SimpleMonitor EnsureMonitorInitialized() {
      return monitor ??= new SimpleMonitor(this);
    }

    #endregion Private Methods

    #region Private Class

    private sealed class SimpleMonitor : IDisposable {

      #region Private Fields

      private readonly LockingObservableCollection<T> collection;

      #endregion Private Fields

      #region Constructor

      public SimpleMonitor(LockingObservableCollection<T> collection) {
        this.collection = collection;
      }

      #endregion Constructor

      #region IDisposable Implementation

      public void Dispose() => collection.blockReentrancyCount--;

      #endregion IDisposable Implementation

    }

    #endregion Private Class

  }

  internal static class EventArgsCache {

    internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");

    internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");

    internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

  }

}
