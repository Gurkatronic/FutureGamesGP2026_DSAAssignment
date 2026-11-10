using Environment.Tasks;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LinearSearchTask", menuName = "Scriptable Objects/LinearSearchTask")]
public class LinearSearchTask : Task {
    public override void Execute() {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Searching an ARRAY of type T for a search value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="data"></param>
    /// <param name="searchValue"></param>
    /// <param name="sortMethod"></param>
    public void Execute<T, R, E>(T[] data, R searchValue, E sortMethod) where T : IClassCompare<T, E> {
        bool found = false;
        for(int i = 0; i < data.Length; i++) {
            if(data[i].CompareData(searchValue, sortMethod) == 0) {
                found = true;
                Debug.Log("Found matching data at index " + i);
                break;
            }
        }

        if(!found)
            Debug.Log("Could not find matching data with search " + searchValue.ToString());
    }

    /// <summary>
    /// Searching an LIST of type T for a search value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="data"></param>
    /// <param name="searchValue"></param>
    /// <param name="sortMethod"></param>
    public void Execute<T, R, E>(List<T> data, R searchValue, E sortMethod) where T : IClassCompare<T, E> {
        bool found = false;
        for(int i = 0; i < data.Count; i++) {
            if(data[i].CompareData(searchValue, sortMethod) == 0) {
                found = true;
                Debug.Log("Found matching data at index " + i);
                break;
            }
        }

        if(!found)
            Debug.Log("Could not find matching data with search " + searchValue.ToString());
    }
}
