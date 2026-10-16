using Environment.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {

        public Hög<Task> computerStack = new Hög<Task>();
        public Kö<Task> computerTasks = new Kö<Task>();
        public Glist<Task> testGlist = new Glist<Task>();
        [SerializeField] SaveTask saveTask;
        [SerializeField] LoadTask loadTask;
        [SerializeField] SortTask sortTask;

        public Glist<string> myLista = new Glist<string>();
        

        public PopText popText;


        private void Awake() {

            ComputerQueTasks();
        }

        public void ComputerQueTasks() {
            //
            computerTasks.LineUp(saveTask);
            computerTasks.LineUp(loadTask);

            //
            computerStack.Pack(loadTask);
            computerStack.Pack(saveTask);
        }

        public void AddTaskQue(Task newTask) {
            computerTasks.LineUp(newTask);
        }

        public void OnInteract() {
            while(computerTasks.Count > 0)
                computerTasks.Enter().Execute();

            if(computerTasks.Count <= 0)
                Debug.Log("Computer is out of tasks");

        }

        /*public void OnInteract() {
            while(computerStack.Count > 0) {
                computerStack.Pop
            }
        }*/
    }
}
