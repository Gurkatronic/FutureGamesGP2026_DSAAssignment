using Environment.Tasks;
using System.Collections.Generic;
using System.Linq;
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

    #region array quicksort
    public void ExecuteArray<T, E>(T[] dataArray, E sortMethod) where T : IClassCompare<T,E>{
        int low = 0;
        int high = dataArray.Length-1;

        QuickSortArray(dataArray, low, high, sortMethod);
    }

    void QuickSortArray<T,E>(T[] data, int low, int high, E sortMethod) where T : IClassCompare<T,E> {
        if(low >= high) return;

        int pivot = PartionSortArray(data, low, high, sortMethod);

        QuickSortArray(data, low, pivot - 1, sortMethod);
        QuickSortArray(data, pivot + 1, high, sortMethod);
    }

    int PartionSortArray<T,E>(T[] data, int low, int high, E sortMethod) where T : IClassCompare<T, E> {
        T compareData = data[high];

        int pointer = low - 1;

        for(int i = low; i < high; i++) {
            if(data[i].CompareThis(compareData, sortMethod) < 0) {
                pointer++;
                (data[i], data[pointer]) = (data[pointer], data[i]);
            }
        }
        pointer++;

        (data[pointer], data[high]) = (data[high], data[pointer]);

        return pointer;
    }
    #endregion

    #region list quicksort
    public void ExecuteList<T, E>(List<T> dataArray, E sortMethod) where T : IClassCompare<T, E> {
        int low = 0;
        int high = dataArray.Count - 1;

        QuickSortList(dataArray, low, high, sortMethod);
    }

    void QuickSortList<T, E>(List<T> data, int low, int high, E sortMethod) where T : IClassCompare<T, E> {
        if(low >= high) return;

        int pivot = PartionSortList(data, low, high, sortMethod);

        QuickSortList(data, low, pivot - 1, sortMethod);
        QuickSortList(data, pivot + 1, high, sortMethod);
    }

    int PartionSortList<T, E>(List<T> data, int low, int high, E sortMethod) where T : IClassCompare<T, E> {
        T compareData = data[high];

        int pointer = low - 1;

        for(int i = low; i < high; i++) {
            if(data[i].CompareThis(compareData, sortMethod) < 0) {
                pointer++;
                (data[i], data[pointer]) = (data[pointer], data[i]);
            }
        }
        pointer++;

        (data[pointer], data[high]) = (data[high], data[pointer]);

        return pointer;
    }
    #endregion
    public override void Execute() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Get a array of what the original list index would look like sorted
    /// </summary>
    /// <param name="dataList"></param>
    /// <param name="sortMethod"></param>
    /// <returns></returns>
    public int[] Execute(Tenant[] dataList, TenantSortMethod sortMethod) {

        return QuickSortIndex(dataList, sortMethod);
    }
    int[] QuickSortIndex<T, E>(T[] array, E sortMethod) where T : IClassCompare<T, E> {

        int[] sortedIndexArray = new int[array.Length];

        for(int i = 0; i < sortedIndexArray.Length; i++) {
            sortedIndexArray[i] = i;
        }

        QuickSortInternal(sortedIndexArray, array, 0, array.Length-1, sortMethod);

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
            if(array[indexArray[i]].CompareThis(compareObj, sortMethod) < 0) {
                pointer++;
                (indexArray[i], indexArray[pointer]) = (indexArray[pointer], indexArray[i]);
            }
        }

        pointer++;
        (indexArray[high], indexArray[pointer]) = (indexArray[pointer], indexArray[high]);

        return pointer;
    }
}
