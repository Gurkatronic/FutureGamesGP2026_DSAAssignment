using Environment.Tasks;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadTask", menuName = "Tasks/LoadTask")]
public class LoadTask : Task
{
    public override void Execute() {
        var fileNames = Directory.GetFiles(Application.persistentDataPath, "*.json");
        PersonClassList personClassList = new();

        foreach(string fileName in fileNames) {
            string fileText = File.ReadAllText(fileName);
            personClassList = JsonUtility.FromJson<PersonClassList>(fileText);
        }

        if(personClassList == null) {
            Debug.Log("Could not load person class list");
            return;
        }

        SortTask newSort = new(SortType.Firstname);

        personClassList = newSort.Execute(personClassList);

        //GameManager.Instance.mainComputer.CreateStack(personClassList);
        foreach(PersonClass person in personClassList.listOfPersons) {
            PersonTask newTask = new(person);
            GameManager.Instance.mainComputer.AddTaskQue(newTask);
        }

        Debug.Log("Loaded");
            
    }
}
