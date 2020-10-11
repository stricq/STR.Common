using System.Collections.Specialized;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Str.Common.Core;


namespace Str.Common.Tests {

  [TestClass]
  public class LockingObservableCollectionTests {

    #region OnCollectionChanged Tests

    [TestMethod, TestCategory("Unit")]
    public void OnCollectionChangedAddEventTest() {
      TestClass tester = new TestClass();

      LockingObservableCollection<TestClass> testCollection = new LockingObservableCollection<TestClass>();

      int changedCount = 0;

      testCollection.CollectionChanged += (sender, args) => {
        if (args.Action == NotifyCollectionChangedAction.Add) ++changedCount;
      };

      testCollection.Add(tester);

      Assert.AreEqual(1, changedCount);
    }

    [TestMethod, TestCategory("Unit")]
    public void OnCollectionChangedRemoveEventTest() {
      TestClass tester1 = new TestClass();
      TestClass tester2 = new TestClass();

      LockingObservableCollection<TestClass> testCollection = new LockingObservableCollection<TestClass>();

      int changedCount = 0;

      testCollection.CollectionChanged += (sender, args) => {
        if (args.Action == NotifyCollectionChangedAction.Remove) ++changedCount;
      };

      testCollection.Add(tester1);
      testCollection.Add(tester2);

      testCollection.Remove(tester1);

      Assert.AreEqual(1, changedCount);
    }

    [TestMethod, TestCategory("Unit")]
    public void OnCollectionChangedResetEventTest() {
      TestClass tester1 = new TestClass();
      TestClass tester2 = new TestClass();

      LockingObservableCollection<TestClass> testCollection = new LockingObservableCollection<TestClass>();

      int changedCount = 0;

      testCollection.CollectionChanged += (sender, args) => {
        if (args.Action == NotifyCollectionChangedAction.Reset) ++changedCount;
      };

      testCollection.Add(tester1);
      testCollection.Add(tester2);

      testCollection.Clear();

      Assert.AreEqual(1, changedCount);
    }

    [TestMethod, TestCategory("Unit")]
    public void OnCollectionChangedReplaceEventTest() {
      TestClass tester1 = new TestClass();
      TestClass tester2 = new TestClass();

      LockingObservableCollection<TestClass> testCollection = new LockingObservableCollection<TestClass>();

      int changedCount = 0;

      testCollection.CollectionChanged += (sender, args) => {
        if (args.Action == NotifyCollectionChangedAction.Replace) ++changedCount;
      };

      testCollection.Add(tester1);

      testCollection[0] = tester2;

      Assert.AreEqual(1, changedCount);
    }

    [TestMethod, TestCategory("Unit")]
    public void OnCollectionChangedMoveEventTest() {
      TestClass tester1 = new TestClass();
      TestClass tester2 = new TestClass();

      LockingObservableCollection<TestClass> testCollection = new LockingObservableCollection<TestClass>();

      int changedCount = 0;

      testCollection.CollectionChanged += (sender, args) => {
        if (args.Action == NotifyCollectionChangedAction.Move) ++changedCount;
      };

      testCollection.Add(tester1);
      testCollection.Add(tester2);

      testCollection.Move(0, 1);

      Assert.AreEqual(1, changedCount);
    }

    #endregion OnCollectionChanged Tests

    #region Private Class

    private class TestClass { }

    #endregion Private Class

  }

}
