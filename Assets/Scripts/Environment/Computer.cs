using Environment.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        public static Computer Instance;
        public Hög<Task> computerStack = new Hög<Task>();
        public Kö<Task> computerTasks = new Kö<Task>();
        public Glist<Task> testGlist = new Glist<Task>();

        [SerializeField] Task testTask;
        public Glist<string> myLista = new Glist<string>();
        public PopText popText;

        [SerializeField] DollyCameraScript dollyCameraScript;
        [SerializeField] TenantsData tenantsData;
        [SerializeField] TenantListView tenantListView;


        private void Awake() {

            if(Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
            computerTasks.LineUp(testTask);
        }

        public void QueCodeReviewTask() {
        }

        public void ComputerQueTasks() {
            
        }

        public void AddTaskQue(Task newTask) {
            computerTasks.LineUp(newTask);
        }

        [ContextMenu("Interact")]
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

        [ContextMenu("Test listview")]
        void TestListView() {
            tenantListView.LoadData(tenantsData);
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

        public void ReciveTenants(TenantsData data) {
            Debug.Log("Reciving list of tenants");
            tenantsData = data;
        }
    }
}
