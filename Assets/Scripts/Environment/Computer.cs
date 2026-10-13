using Environment.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        
        [SerializeField] public Stack<PersonTask> testStack = new Stack<PersonTask>();
        [SerializeField] public Queue<Task> computerTasks = new Queue<Task>();
        [SerializeField] SaveTask saveTask;
        [SerializeField] LoadTask loadTask;
        [SerializeField] SortTask sortTask;
        //denna ska nog vara en stack med flera olika person tasks.
        Hashtable testHash = new Hashtable();
        PersonClassList personsLoaded;

        public PopText popText;


        private void Awake() {
            ComputerQueTasks();
        }

        public void ComputerQueTasks() {
            computerTasks.Enqueue(saveTask);
            computerTasks.Enqueue(loadTask);
        }

        public void AddTaskQue(Task newTask) {
            computerTasks.Enqueue(newTask);
        }

        public void OnInteract() {
            while(computerTasks.Count > 0)
                computerTasks.Dequeue().Execute();
        }
    }
}
