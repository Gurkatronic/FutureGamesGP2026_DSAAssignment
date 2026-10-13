using Environment;
using UnityEngine;
using System.IO;
using System.Linq;

public class SaveComputer : MonoBehaviour, IInteractable
{
    [SerializeField] SaveTask saveTask;
    //string[] firstNames = new string[] { "Anton", "Gurkan", "Boss", "Donbossen", "bitbasedhuman" };
    //string[] lastNames = new string[] { "Gerklev", "Urga", "Uuuuga", "boga", "1010101" };
    //string saveName = "Persons.json";
    public void OnInteract() {
        saveTask.Execute();
        ////save persons into json file
        ////persisentettestatatat path
        //PersonClass[] personClass = new
        //                            PersonClass[Mathf.Min(firstNames.Length, lastNames.Length)];
        //string jsonSaveString = "";
        //PersonClassList personClassList = new();
        //personClassList.listOfPersons = personClass;

        //for(int i = 0; i < firstNames.Length && i < lastNames.Length; i++) {
        //    PersonClass newPerson = new(UnityEngine.Random.Range(10, 45),
        //                                firstNames[Random.Range(0, firstNames.Length)],
        //                                lastNames[Random.Range(0, lastNames.Length)]);
        //    personClass[i] = newPerson;
        //}

        //jsonSaveString = JsonUtility.ToJson(personClassList, true);
        //string savePath = Path.Combine(Application.persistentDataPath, saveName);
        //File.WriteAllText(savePath, jsonSaveString);
    }
}
