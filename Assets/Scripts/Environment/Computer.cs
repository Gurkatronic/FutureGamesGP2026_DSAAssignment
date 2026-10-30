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
        [SerializeField] CodeReviewTask codeReviewTask;

        public Glist<string> myLista = new Glist<string>();
        public PopText popText;

        [SerializeField] DollyCameraScript dollyCameraScript;


        private void Awake() {

            //ComputerQueTasks();
            QueCodeReviewTask();
        }

        public void QueCodeReviewTask() {
            computerTasks.LineUp(codeReviewTask);
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
            //Debug.Log("Interact pc");
            //GameManager.Instance.playerController.DisableControls();
            //dollyCameraScript.MoveTowards();
            //dollyCameraScript.destinationArrived += ActivatePcUI;
            while(computerTasks.Count > 0)
                computerTasks.Enter().Execute();

            if(computerTasks.Count <= 0)
                Debug.Log("Computer is out of tasks");

        }

        void ActivatePcUI() {
            dollyCameraScript.destinationArrived -= ActivatePcUI;
            GameManager.Instance.uiController.ShowPc();
        }

        void DeactivatePcUI() {
            GameManager.Instance.uiController.ShowPc(false);
        }

        public void LeavePc() {
            DeactivatePcUI();
            GameManager.Instance.playerController.ActivateControls();
            dollyCameraScript.MoveBack();
        }

        /*public void OnInteract() {
            while(computerStack.Count > 0) {
                computerStack.Pop
            }
        }*/
    }
}
