using Environment.Tasks;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BinarySearchTask", menuName = "Scriptable Objects/BinarySearchTask")]
public class BinarySearchTask : Task {
    public override void Execute() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Search a array for the data value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="data"></param>
    /// <param name="searchValue"></param>
    /// <param name="sortMethod"></param>
    public void Execute<T, R, E>(T[] data, R searchValue, E sortMethod) where T : IClassCompare<T, E> {
        int size = data.Length;

        int low = 0;
        int high = size - 1;

        while(low <= high) {
            int middle = (low+high) / 2;
            int compareValue = data[middle].CompareData(searchValue, sortMethod);

            if(compareValue == 0) {
                Debug.Log("Found data with class: " + data[middle].PrintClass());
                break;
            }

            if(compareValue < 0) 
                low = middle + 1;
            else 
                high = middle - 1;

        }
    }

    /// <summary>
    /// Search a list for the data value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="data"></param>
    /// <param name="searchValue"></param>
    /// <param name="sortMethod"></param>
    public void Execute<T, R, E>(List<T> data, R searchValue, E sortMethod) where T : IClassCompare<T, E> {
        int size = data.Count;

        int low = 0;
        int high = size - 1;
        int middle = (high - low) / 2;

        while(low < high) {
            int compareValue = data[middle].CompareData(searchValue, sortMethod);

            if(compareValue == 0) {
                Debug.Log("Found data with class: " + data[middle].PrintClass());
                break;
            }

            if(compareValue < 0) 
                low = middle + 1;
            else 
                high = middle - 1;

            middle = (high - low) / 2;
        }
    }
}
