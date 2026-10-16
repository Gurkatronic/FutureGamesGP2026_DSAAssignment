using UnityEngine;
using System;

/// <summary>
/// This a hög it keeps things in a pile
/// </summary>
public class Hög<T>
{

    public int Count { get { return contents.Count; } }
    public Glist<T> contents;

    public Hög() {
        contents = new();
    }

    public void Pack(T content) {
        contents.Add(content);
    }

    public T Top() {
        if(contents.Count <= 0) {
            throw new Exception("Hög is empty nothing.");
        }
        return contents.RemoveLast();
    }
}
