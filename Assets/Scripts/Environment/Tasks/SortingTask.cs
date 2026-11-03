using Environment.Tasks;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public enum TenantSortMethod {
    Name,
    AptNum,
    ExitDate,
    EnterDate
}

[CreateAssetMenu(fileName = "SortingTask", menuName = "Tasks/SortingTask")]
public class SortingTask : Task
{
    public int[] Execute(Tenant[] dataList,TenantSortMethod sortMethod) {
        

        return QuickSortIndex(dataList, sortMethod);
    }

    public override void Execute() {
        throw new System.NotImplementedException();
    }

    int[] QuickSortIndex<T, E>(T[] array, E sortMethod) where T : IClassCompare<T, E> {

        int[] sortedIndexArray = new int[array.Length];

        for(int i = 0; i < sortedIndexArray.Length; i++) {
            sortedIndexArray[i] = i;
        }

        QuickSortInternal(sortedIndexArray, array, 0, array.Length - 1, sortMethod);

        return sortedIndexArray;
    }

    void QuickSortInternal<T, E>(int[] indexArray, T[] array, int low, int high, E sortMethod) where T : IClassCompare<T,E> {
        if(low >= high) return;

        int pivot = PartionSort(indexArray, array, low, high, sortMethod);

        QuickSortInternal(indexArray, array, low, pivot - 1, sortMethod);
        QuickSortInternal(indexArray, array, pivot + 1, high, sortMethod);

    }

    int PartionSort<T,E>(int[] indexArray, T[] array, int low, int high, E sortMethod) where T : IClassCompare<T,E> {
        T compareObj = array[indexArray[high]];

        int pointer = low - 1;

        for(int i = low; i < high; i++) {
            if(array[indexArray[i]].CompareThis(compareObj, sortMethod) > 0) {
                pointer++;
                (indexArray[i], indexArray[pointer]) = (indexArray[pointer], indexArray[i]);
            }
        }

        pointer++;
        (indexArray[pointer], indexArray[high]) = (indexArray[pointer], indexArray[high]);

        return pointer;
    }
}
