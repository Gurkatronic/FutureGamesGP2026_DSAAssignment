using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class TenantsData
{
    public List<Tenant> allTenants = new List<Tenant>();

}

[Serializable]
public class Tenant { //ska konvertera värden i klassen också
    public string name;
    public string firstName; // Splittar stringen name till first och last name
    public string lastName;
    public int apartmentNum;
    public bool hasEngineRoomClearance;
    public string lastEntranceInEngineRoom;
    public DateTime parsedLastEntrance;
    public string lastExitFromEngineRoom;
    public DateTime parsedLastExit;

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
}
