using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// totally not a Queue... this one is a perfected concotion of masterful code. No way to crash it... *pinky promise*
/// </summary>
/// <typeparam name="T">The type of items allowed to Que... I mean Kö up.</typeparam>
public class Kö<T>
{
    Glist<T> contents;
    /// <summary>
    /// Hire a bouncer and roll out the red carpet.
    /// </summary>
    public Kö() {
        contents = new Glist<T>();
    }
    /// <summary>
    /// Check the amount of items in the Kö
    /// </summary>
    /// <returns>There is this many in the Kö</returns>
    public int Count() {
        return contents.Count;
    }

    /// <summary>
    /// Put something into the Kö
    /// </summary>
    /// <param name="content">Step up</param>
    public void LineUp(T content) {
        contents.Add(content);
    }

    /// <summary>
    /// This item is allowed to enter
    /// </summary>
    /// <returns>A quick glance at the one entering</returns>
    public T Enter() {
        return contents.Pop();
    }

    /// <summary>
    /// Does this item have the right to enter?
    /// </summary>
    /// <returns>hmmm the image is a bit blurry...</returns>
    /// <exception cref="System.Exception"></exception>
    public T IdCheck() {
        if(contents.Count <= 0)
            throw new System.Exception("The Kö is empty");

        return contents[0];
    }
}
