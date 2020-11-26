using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Str.Common.Core;
using Str.Common.Extensions;


namespace Str.Common.Tests {

  [TestClass]
  public class LockingReadOnlyCollectionTests {

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionCount() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      Assert.AreEqual(source.Count, tester.Count);
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionContains() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      Assert.IsTrue(tester.Contains(2));
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionCopyTo() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      int[] array = new int[3];

      tester.CopyTo(array, 0);

      Assert.AreEqual(source[0], array[0]);
      Assert.AreEqual(source[1], array[1]);
      Assert.AreEqual(source[2], array[2]);
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionIsReadOnly() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      Assert.IsTrue(tester.IsReadOnly);
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionIsEnumerable() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      int total = tester.Sum();

      Assert.AreEqual(6, total);
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionIndexOf() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      int index = tester.IndexOf(3);

      Assert.AreEqual(2, index);
    }

    [TestMethod, TestCategory("Unit")]
    public void LockingReadOnlyCollectionIndexer() {
      List<int> source = new List<int> { 1, 2, 3 };

      LockingReadOnlyCollection<int> tester = source.ToLockingReadOnlyCollection();

      Assert.AreEqual(source[0], tester[0]);
      Assert.AreEqual(source[1], tester[1]);
      Assert.AreEqual(source[2], tester[2]);
    }

  }

}
