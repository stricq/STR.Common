namespace Str.Common.Core;


public static class CartesianProduct {

    public static IEnumerable<List<T>> Build<T>(List<List<T>> sequences) {
        if (sequences.Count == 0) yield break;
        //
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        //
        foreach(List<T> seq in sequences) {
            if (seq.Count == 0) yield break;
        }

        int[] indices = new int[sequences.Count];

        while(true) {
            //
            // Yield the current combination
            //
            yield return indices.Select((x, i) => sequences[i][x]).ToList();
            //
            // Increment indices from the last list backwards
            //
            int k = sequences.Count - 1;

            while(k >= 0) {
                indices[k]++;

                if (indices[k] < sequences[k].Count) break;

                indices[k] = 0;

                k--;
            }

            if (k < 0) yield break;
        }
    }

}
