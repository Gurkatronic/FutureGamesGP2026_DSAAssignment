using System;
using UnityEngine;
using System.Collections.Generic;

//made this interface to implicitly tell what types we are comparing instead of unboxing and boxing objects

/// <summary>
/// Comparison interface
/// </summary>
/// <typeparam name="T">The type of class</typeparam>
/// <typeparam name="E">The enumerator to pick what field to look at</typeparam>
public interface IClassCompare<T, E> {
    public abstract int CompareThis(T b, E e);
    public abstract int CompareData<D>(D data, E e);
}


[Serializable]
public class TenantsData
{
    public List<Tenant> allTenants = new List<Tenant>();
}

[Serializable]
public class Tenant : IClassCompare<Tenant, TenantSortMethod> { //ska konvertera värden i klassen också
    public string name;
    public string firstName; // Splittar stringen name till first och last name
    public string lastName;
    public int apartmentNum;
    public bool hasEngineRoomClearance;
    public string lastEntranceInEngineRoom;
    public DateTime parsedLastEntrance;
    public string lastExitFromEngineRoom;
    public DateTime parsedLastExit;

    public int CompareThis(Tenant b, TenantSortMethod e) {
        int returnValue = 0;
        switch(e) {
            case TenantSortMethod.Name:
                returnValue = CompareValues(firstName, b.firstName);
                break;
            case TenantSortMethod.AptNum:
                returnValue = CompareValues(apartmentNum, b.apartmentNum);
                break;
            case TenantSortMethod.ExitDate:
                returnValue = CompareValues(parsedLastExit, b.parsedLastExit);
                break;
            case TenantSortMethod.EnterDate:
                returnValue = CompareValues(parsedLastEntrance, b.parsedLastEntrance);
                break;
        }

        return returnValue;
    }

    int CompareValues(int a, int b) {
        if(a > b)
            return 1;
        else if(a < b)
            return -1;
        else
            return 0;
    }
    int CompareValues(DateTime a, DateTime b) {
        return DateTime.Compare(a, b);
    }

    int CompareValues(string a, string b) {
        return string.Compare(a, b);
    }


    public void InitializeData() {
        InitializeName();
        InitializeDate();
    }

    void InitializeApartment() {
        //kanske gör här
    }

    void InitializeDate() {
        parsedLastEntrance = DateTime.Parse(lastEntranceInEngineRoom);
        parsedLastExit = DateTime.Parse(lastExitFromEngineRoom);
        //parsedLastEntrance = lastEntranceInEngineRoom.ToString("yyyy, MM, dd" + ".");
        //parsedLastExit = lastExitFromEngineRoom.ToString("yyyy, MM, dd" + ".");
    }

    void InitializeName() {
        string[] splitName = name.Split(" ");
        if(splitName.Length < 2) {
            Debug.LogWarning($"Tenant ({name}) name does follow correct naming");
            firstName = "Jane";
            lastName = "Doe";
        }
        else {
            firstName = splitName[0];
            lastName = splitName[1];
        }
    }

    public int CompareData<D>(D data, TenantSortMethod e) {
        switch(e) {
            case TenantSortMethod.Name:
                return firstName.CompareTo(data);
            case TenantSortMethod.AptNum:
                return apartmentNum.CompareTo(data);
            case TenantSortMethod.ExitDate:
                return parsedLastExit.CompareTo(data);
            case TenantSortMethod.EnterDate:
                return parsedLastEntrance.CompareTo(data);
        }

        Debug.Log("Wrong enum value. This should not happen");
        return -2;
    }
}
