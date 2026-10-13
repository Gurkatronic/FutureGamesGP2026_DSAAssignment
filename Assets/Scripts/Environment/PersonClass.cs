using UnityEngine;

[System.Serializable]
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
