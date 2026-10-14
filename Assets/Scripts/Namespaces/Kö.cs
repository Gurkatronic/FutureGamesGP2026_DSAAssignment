using System.Collections.Generic;
using UnityEngine;

public class Kö<T>
{
    Glist<T> contents;

    public Kö() {
        contents = new Glist<T>();
    }

    public int Count() {
        return contents.Count;
    }

    public void LineUp(T content) {
        contents.Add(content);
    }

    public T Enter() {
        return contents.Pop();
    }

    public T IdCheck() {
        return contents[0];
    }
}
