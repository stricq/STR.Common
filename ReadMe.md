# STR.Common
Common code for STR Applications

### Included Code
* Enumeration class from [Jimmy Bogard](https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/)
* CopyToAsync extension method from [Nicholas Carey](https://stackoverflow.com/questions/1540658/net-asynchronous-stream-read-write/4139427#4139427)
* HasMinimalDifference extension method from [MSDN](https://docs.microsoft.com/en-us/dotnet/api/system.double.equals)
* ITraversable interface and Traverse extension method from unknown

### Locking Collections

* LockingList and LockingEnumerator from [Tion](https://codereview.stackexchange.com/questions/7276/reader-writer-collection)
* LockingCollection based on LockingList.
* LockingObservableCollection based on LockingCollection.

### Miscellaneous Extensions
#### IEnumerable
* ForEach
* ForEachAsync
* ToLockingList
* ToLockingCollection
* ToLockingObservableCollection

#### HttpContent and HttpWebResponse
* GetResponseStreamWithDecompressionAsync
* GetResponseStreamWithDecompression

#### (Locking)ObservableCollection
* AddRange
* Sort
* OrderedMerge

#### Task
* Fire
* FireAndForget
* FireAndWait
* RunOnUiThread
