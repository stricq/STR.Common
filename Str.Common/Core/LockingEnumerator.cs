using System.Collections;
using System.Collections.Generic;
using System.Threading;


namespace Str.Common.Core {

  // https://codereview.stackexchange.com/questions/7276/reader-writer-collection

  public class LockingEnumerator<T> : IEnumerator<T> {

    #region Private Fields

    private readonly IEnumerator<T> inner;

    private readonly ReaderWriterLockSlim innerLock;

    #endregion Private Fields

    #region Constructor

    public LockingEnumerator(IEnumerator<T> inner, ReaderWriterLockSlim innerLock) {
      this.inner = inner;

      this.innerLock = innerLock;

      innerLock.EnterReadLock();
    }

    #endregion Constructor

    #region IEnumerator<T> Implementation

    public bool MoveNext() {
      return inner.MoveNext();
    }

    public void Reset() {
      inner.Reset();
    }

    public T Current => inner.Current;

    object IEnumerator.Current => Current!;

    #endregion IEnumerator<T> Implementation

    #region IDisposable Implementation

    public void Dispose() {
      innerLock.ExitReadLock();
    }

    #endregion IDisposable Implementation

  }

}
