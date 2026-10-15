using Environment.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        
        public Stack<PersonTask> testStack = new Stack<PersonTask>();
        public Kö<Task> computerTasks = new Kö<Task>();
        public Glist<Task> testGlist = new Glist<Task>();
        [SerializeField] SaveTask saveTask;
        [SerializeField] LoadTask loadTask;
        [SerializeField] SortTask sortTask;

        public Glist<string> myLista = new Glist<string>();
        

        public PopText popText;


        private void Awake() {

            ComputerQueTasks();
            testGlist.Add(saveTask);
            testGlist.Add(saveTask);
            testGlist.Add(loadTask);
            testGlist.Add(saveTask);
            testGlist.PrintContent();


        }

        public void ComputerQueTasks() {
            computerTasks.LineUp(saveTask);
            computerTasks.LineUp(loadTask);
            
        }

        public void AddTaskQue(Task newTask) {
            computerTasks.LineUp(newTask);
        }

        public void OnInteract() {
            while(computerTasks.Count() > 0)
                computerTasks.Enter().Execute();

            if(computerTasks.Count() <= 0)
                Debug.Log("Computer is out of tasks");

        }
    }
}
