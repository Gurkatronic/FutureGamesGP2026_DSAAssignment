using Environment.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        
        [SerializeField] public Stack<PersonTask> testStack = new Stack<PersonTask>();
        public PopText popText;


        private void Awake() {
            //CreateStack();
        }

        public void CreateStack(PersonClassList personClassList) {
            if(personClassList == null) {
                Debug.Log("THIS ONE IS EMPTY LOL");
                return;
            }

            foreach(PersonClass person in personClassList.listOfPersons) {
                PersonTask newTask = new();
                newTask.person = person;
                testStack.Push(newTask);
            }
        }

        public void OnInteract() {
            if(testStack.Count > 0)
                testStack.Pop().Execute();
            else
                Debug.Log("STACK EMPTY");

        }
    }
}
