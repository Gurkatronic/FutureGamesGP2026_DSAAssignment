using System.Collections.Generic;
using System;
using UnityEngine;

public enum SortType {
    Firstname,
    Lastname,
    Age
}

public class PersonClassList {
    public PersonClass[] listOfPersons;
}

[Serializable]
public class PersonClass
{
    public int age;
    public string firstName;
    public string lastName;

    public PersonClass(int age, string firstName, string lastName) {
        this.age = age;
        this.firstName = firstName;
        this.lastName = lastName;
    }
}
