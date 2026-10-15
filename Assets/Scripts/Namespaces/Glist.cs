using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Glist<T>
{
    int startSize = 1;
    int currentPos = 0;
    int sizeMultiplier = 2;
    public int Count { get => currentPos; }
    T[] contents;
    IComparer<T> comparer;
    public T this[int index] {
        get {
            return contents[index];
        }
        set {
            contents[index] = value;
        }
    }

    public Glist() {
        contents = new T[startSize];
        comparer = Comparer<T>.Default;
        
    }
    
    /// <summary>
    /// Add a new item to the Glist
    /// </summary>
    /// <param name="content">The new item</param>
    public void Add(T content) {
        if(currentPos == contents.Length - 1) 
            ResizeList();

        contents[currentPos] = content;
        currentPos++;

    }
    /// <summary>
    /// Search the glist for this 'content' and remove that instance from the Glist
    /// </summary>
    /// <param name="content">The item to remove in the Glist</param>
    public void WipeThis(T content) {
        for(int i = 0; i < contents.Length; i++) {
            Debug.Log(comparer.Compare(contents[i], content));
            if(comparer.Compare(contents[i], content) == 0) {
                //if match hit here take the other end and merge it into the list
                for(int x = i+1; x < contents.Length; x++) {
                    contents[x-1] = contents[x];
                }
                currentPos--;
            }
        }
    }


    public void PrintContent() {
        for(int i = 0; i < contents.Length; i++) {
            if(contents[i] != null)
                Debug.Log(contents[i]);
        }
    }

    /// <summary>
    /// This increases the size of the array that is the fake list under the hood
    /// </summary>
    void ResizeList() {
        T[] newArray = new T[currentPos*sizeMultiplier];

        for(int i = 0; i < contents.Length; i++) {
            newArray[i] = contents[i];
        }
        contents = newArray;

        //Debug.Log("New size is: " + contents.Length);
    }

    /// <summary>
    /// Remove the last added element of the list
    /// </summary>
    /// <returns>Return the item that was removed from the Glist</returns>
    public T Pop() {
        T tempCont = contents[0];
        contents[0] = default(T);

        for(int i = 1; i < currentPos; i++) {
            contents[i - 1] = contents[i];
        }

        currentPos--;
        return tempCont;
    }

    /// <summary>
    /// Clear everything in the Glist
    /// </summary>
    public void Clear() {
        contents = new T[startSize];
        currentPos = 0;
    }

    public T LastElement() {
        return contents[Count - 1];
    }

}
