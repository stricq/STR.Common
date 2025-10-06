using System.Diagnostics.CodeAnalysis;

using Str.Common.Contracts;
using Str.Common.Core;
using Str.Common.Extensions;


namespace Str.Common.Tests;


[TestClass]
public class TraverseTests {

    #region Private Fields

    private static LockingObservableCollection<TestClass> root;

    #endregion Private Fields

    #region Class Initialize

    [ClassInitialize]
    public static void ClassInit(TestContext _) {
        root = [];

        TestClass branch1 = new() { Value = 1 };
        TestClass branch2 = new() { Value = 2 };

        root.Add(branch1);
        root.Add(branch2);

        TestClass leaf1 = new() { Value = 3 };
        TestClass leaf2 = new() { Value = 4 };

        branch1.Children.Add(leaf1);
        branch1.Children.Add(leaf2);

        TestClass leaf3 = new() { Value = 5 };
        TestClass leaf4 = new() { Value = 6 };

        branch2.Children.Add(leaf3);
        branch2.Children.Add(leaf4);
    }

    #endregion Class Initialize

    #region Unit Tests

    [TestMethod, TestCategory("Unit")]
    public void TraverseTestNoPredicate() {
        IEnumerable<TestClass> flat = root.Traverse();

        Assert.IsNotNull(flat);

        Assert.AreEqual(6, flat.Count());
    }

    [TestMethod, TestCategory("Unit")]
    public void TraverseTestWithPredicate() {
        IEnumerable<TestClass> flat = root.Traverse(tc => tc.Value == 3 || tc.Value == 5);

        Assert.IsNotNull(flat);

        Assert.AreEqual(2, flat.Count());
    }

    [TestMethod, TestCategory("Unit")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
    public void TraverseTestEmptyTreeNoPredicate() {
        LockingObservableCollection<TestClass> test = [];

        IEnumerable<TestClass> flat = test.Traverse();

        Assert.IsNotNull(flat);

        Assert.AreEqual(0, flat.Count());
    }

    [TestMethod, TestCategory("Unit")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
    public void TraverseTestEmptyTreeWithPredicate() {
        LockingObservableCollection<TestClass> test = [];

        IEnumerable<TestClass> flat = test.Traverse(tc => tc.Value == 1);

        Assert.IsNotNull(flat);

        Assert.AreEqual(0, flat.Count());
    }

    #endregion Unit Tests

    #region Test Class

    public class TestClass : ITraversable<TestClass> {

        public int Value { get; init; }

        public LockingObservableCollection<TestClass> Children { get; } = [];

        IEnumerable<TestClass> ITraversable<TestClass>.Children => Children;

    }

    #endregion Test Class

}
