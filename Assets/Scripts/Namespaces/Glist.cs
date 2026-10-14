using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Glist<T>
{
    int startSize = 1;
    int currentPos = 0;
    public int Count = 0;
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
    

    public void Add(T content) { // den borde alltid lägga in på 0 i denna funktionen
        //tidigare listan är torkel
        //Lägger in torka och ökar storleken
        //Nu behöver jag ta min tidigare lista och lägga till efter
        if(currentPos == contents.Length - 1) {
            ResizeList((currentPos+1) * 2);
        }
        contents[currentPos] = content;
        currentPos++;
        Count = currentPos;

    }

    public void WipeThis(T content) {
        for(int i = 0; i < contents.Length; i++) {
            Debug.Log(comparer.Compare(contents[i], content));
            if(comparer.Compare(contents[i], content) == 0) {
                //if match hit here take the other end and merge it into the list
                for(int x = i+1; x < contents.Length; x++) {
                    contents[x-1] = contents[x];
                }
                currentPos--;
                Count = currentPos;

            }
        }
    }


    public void PrintContent() {
        for(int i = 0; i < contents.Length; i++) {
            if(contents[i] != null)
                Debug.Log(contents[i]);
        }
    }

    void ResizeList(int newSize) {
        T[] newArray = new T[newSize];

        for(int i = 0; i < contents.Length; i++) {
            newArray[i] = contents[i];
        }
        contents = newArray;

        //Debug.Log("New size is: " + contents.Length);
    }

    public T Pop() {
        T tempCont = contents[0];
        contents[0] = default(T);

        for(int i = 1; i < currentPos; i++) {
            contents[i - 1] = contents[i];
        }

        currentPos--;
        Count = currentPos;
        return tempCont;
    }

    public void Clear() {
        contents = new T[startSize];
        currentPos = 0;
        Count = currentPos;
    }

}
