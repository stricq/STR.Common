using System.Diagnostics.CodeAnalysis;

using Str.Common.Core;


namespace Str.Common.Tests;


[TestClass]
[SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
public class CartesianProductTests {

    [TestMethod, TestCategory("Unit")]
    public void CartesianProductSuccess() {
        List<List<int>> listOfLists = [
            [1, 2],
            [3],
            [4, 5, 6]
        ];

        IEnumerable<IList<int>> product = CartesianProduct.Build(listOfLists);

        int expectedCount = listOfLists.Aggregate(1, (acc, list) => acc * list.Count);

        Assert.AreEqual(expectedCount, product.Count());

        List<List<int>> result = [
            [1, 3, 4],
            [1, 3, 5],
            [1, 3, 6],
            [2, 3, 4],
            [2, 3, 5],
            [2, 3, 6]
        ];

        result.ForEach(expectedCombination => {
            Assert.IsTrue(product.Any(combination => combination.SequenceEqual(expectedCombination)));
        });
    }

}
