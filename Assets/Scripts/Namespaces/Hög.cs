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

    /// <summary>
    /// Place an item on the top of the hög
    /// </summary>
    /// <param name="content"></param>
    public void Pack(T content) {
        contents.Add(content);
    }

    /// <summary>
    /// Pick an item from the top of the hög
    /// </summary>
    /// <returns>Returns the item</returns>
    /// <exception cref="Exception">The hög is empty there is nothing to show</exception>
    public T Top() {
        if(contents.Count <= 0) {
            throw new Exception("Hög is empty nothing.");
        }
        return contents.RemoveLast();
    }
}
