using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PersonTask", menuName = "Tasks/PersonTask")]
public class PersonTask : Task
{
    public PersonClass person;

    public PersonTask(PersonClass personClass) {
        person = personClass;
    }
    public override void Execute() {
        Debug.Log("Age: " + person.age + " Name: " + person.firstName + " Lastname: " + person.lastName);
    }
}
