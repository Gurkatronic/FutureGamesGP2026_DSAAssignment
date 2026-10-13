using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonTask", menuName = "Tasks/PersonTask")]
public class PersonTask : Task
{
    public PersonClass personClass;

    public override void Execute() {

    }

    public PersonClass RetriveClass() {
        return personClass;
    }
}
