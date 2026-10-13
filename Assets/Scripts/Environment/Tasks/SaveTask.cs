using Environment.Tasks;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveTask", menuName = "Tasks/SaveTask")]
public class SaveTask : Task
{
    public string fileName = "Nothing";
    public string fileExtension = ".json";
    public PersonClass[] personClasses;
    public override void Execute() {
        string savePath = Path.Combine(Application.persistentDataPath, fileName+fileExtension);


        PersonClassList personClassList = new();
        personClassList.listOfPersons = personClasses;

        string jsonSaveString = JsonUtility.ToJson(personClassList, true);

        if(File.Exists(savePath))
            File.WriteAllText(savePath, jsonSaveString);
        else {
            using(FileStream sw = File.Create(savePath)) {
                sw.Dispose();
            }
            File.WriteAllText(savePath, jsonSaveString);

        }
        Debug.Log("Saved");
    }
}
