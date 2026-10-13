using Environment;
using UnityEngine;
using System.IO;

public class LoadComputer : MonoBehaviour, IInteractable
{
    [SerializeField]LoadTask loadTask;
    public void OnInteract() {
        loadTask.Execute();
        ////load json file from computer
        ////figure out if its a loadable file
        ////extract data and put into array
        
        //string jsonPathMain = Application.persistentDataPath;
        //var fileNames = Directory.GetFiles(Application.persistentDataPath, "*.json");
        //PersonClassList personClassList = new();

        //foreach(string filename in fileNames) {
        //    string fileText = File.ReadAllText(filename);
        //    personClassList = JsonUtility.FromJson<PersonClassList>(fileText);
        //    Debug.Log(fileText);
        //}

        //Debug.Log("Loaded class list is of len: " + personClassList.listOfPersons.Length);
        //foreach(PersonClass person in personClassList.listOfPersons) {
        //    Debug.Log($"{person.firstName}, {person.lastName}, AGE: {person.age}");
        //}

        //mainPc.CreateStack(personClassList);
    }
}
