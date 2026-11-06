using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchRangeTask", menuName = "Tasks/SearchRangeTask")]
public class SearchRangeTask : Task {
    public override void Execute() {
        
    }
    public (int, int) Execute<T, R, E>(int[]sortedIndex, T[]referenceArray, R lowSearch, R highSearch, E searchMethod) where T: IClassCompare<T,E> {
        Debug.Log($"Low value: {lowSearch.ToString()} high value: {highSearch.ToString()} ");
        Debug.Log("Looping all elements from the sorted index to see if they are sorted");

        int middle = sortedIndex.Length / 2;
        Debug.Log("Ref array len: " + referenceArray.Length);
        int lowest = LowerBoundSearch(sortedIndex, referenceArray, lowSearch, 0, sortedIndex.Length, searchMethod);
        int highest = UpperBoundSearch(sortedIndex, referenceArray, highSearch, 0, sortedIndex.Length, searchMethod);
        Debug.Log($"lowest index {lowest} : Highest index {highest}");
        if(lowest > highest)
            (lowest, highest) = (highest, lowest);

        return (lowest, highest);
    }

    int LowerBoundSearch<T, R, E>(int[] sortedIndex, T[] referenceArray, R searchValue,int low, int high, E searchMethod)where T : IClassCompare<T, E> {

        while(low < high) {
            int middle = low + (high - low) / 2;
            int compareValue = referenceArray[sortedIndex[middle]].CompareData(searchValue, searchMethod);

            if(compareValue < 0) //om mitt värdet är mindre än sök värdet flytter vi fram lägsta värdet
                low = middle + 1;
            else
                high = middle;
        }

        return low; 
    }

    int UpperBoundSearch<T, R, E>(int[] sortedIndex, T[] referenceArray, R searchValue,int low, int high, E searchMethod) where T : IClassCompare<T, E> {

        while(low < high) {
            int middle = low + (high - low) / 2;
            int compareValue = referenceArray[sortedIndex[middle]].CompareData(searchValue, searchMethod);

            if(compareValue <= 0)
                low = middle + 1;
            else
                high = middle;
        }

        return low;
    }

    public void LinSearch<T, R, E>(int[] sortedIndex, T[] referenceArray, R search, E searchMethod) where T: IClassCompare<T, E> {

        for(int i = 0; i < sortedIndex.Length; i++) {
            if(referenceArray[sortedIndex[i]].CompareData(search,searchMethod) == 0) {
                Debug.Log("found value");
                return;
            }
        }
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
