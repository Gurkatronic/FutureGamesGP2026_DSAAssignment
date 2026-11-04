using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchRangeTask", menuName = "Tasks/SearchRangeTask")]
public class SearchRangeTask : Task {
    public override void Execute() {
        
    }
    public (int, int) Execute<T, R, E>(int[]sortedIndex, T[]referenceArray, R lowSearch, R highSearch, E searchMethod) where T: IClassCompare<T,E> {
        int low = BinaryRangeSearch(sortedIndex, referenceArray, lowSearch, searchMethod);
        int high = BinaryRangeSearch(sortedIndex, referenceArray, highSearch, searchMethod);
        Debug.Log($"Range found is at index low {low} and high {high}");
        return (low, high);
    }

    int BinaryRangeSearch<T, R, E>(int[] sortedIndex, T[] referenceArray, R searchValue, E searchMethod) where T: IClassCompare<T,E> {
        int low = 0;
        int high = sortedIndex.Length - 1;

        int middle = (low+high)/2;

        int unitySafety = 0;
        int maxValue = sortedIndex.Length;

        while(low <= high && unitySafety < maxValue) {
            int compareValue = referenceArray[sortedIndex[middle]].CompareData(searchValue, searchMethod);
            if(compareValue == 0) {
                return middle;
            }

            if(compareValue < 0) { //have to use equals here to dodge failstates
                low = middle+1;
            }
            else
                high = middle-1;
    
            middle = (low + high) / 2;
            unitySafety++;
        }

        return -1;
    }
}
