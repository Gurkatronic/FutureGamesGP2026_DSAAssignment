using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchRangeTask", menuName = "Tasks/SearchRangeTask")]
public class SearchRangeTask : Task {
    public override void Execute() {
        
    }
    public int[] Execute<T, R>(int[]sortedIndex, T[]referenceArray, R lowSearch, R highSearch) {
        return sortedIndex;
    }

    void BinaryRangeSearch<T, R>(int[] sortedIndex, T[] referenceArray, R searchValue) {
        //int low 
    }
}
