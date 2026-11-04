using System;
using System.Collections.Generic;
using UnityEngine;

public class TenantListView : MonoBehaviour
{
    [SerializeField] GameObject contentParent;
    [SerializeField] TenantDataBox tenantDataPrefab;

    TenantsData tenantsData;

    [SerializeField] TenantDataBox[] createdBoxes;
    [SerializeField] SortingTask sortingTask;

    Dictionary<TenantSortMethod, int[]> sortedDict = new();



    public void LoadData(TenantsData data) {
        sortedDict = new();
        tenantsData = data;
        FillList();
    }

    public void FillList() {
        createdBoxes = new TenantDataBox[tenantsData.allTenants.Count];
        for(int i = 0; i < tenantsData.allTenants.Count; i++) {
            TenantDataBox newBox = Instantiate(tenantDataPrefab, contentParent.transform);
            newBox.ShowData(tenantsData.allTenants[i]);
            createdBoxes[i] = newBox;
        }
    }

    public void SortBy(int sortType) {
        if(sortingTask != null) {
            HideAll();
            TenantSortMethod sortMethod = (TenantSortMethod)sortType;

            if(sortedDict.ContainsKey(sortMethod)) {
                ShowIndexBased(sortedDict[sortMethod]);
            }
            else {
                int[] sortedIndexes = sortingTask.Execute(tenantsData.allTenants.ToArray(), sortMethod);
                ShowIndexBased(sortedIndexes);
                sortedDict.Add(sortMethod, sortedIndexes);
            } 
        }
    }

    public void TestMethod(TenantSortMethod sortMethod) {

    }

    void ShowIndexBased(int[] index) {
        for(int i = 0; i < index.Length; i++) {
            createdBoxes[i].ShowData(tenantsData.allTenants[index[i]]);
        }
    }

    void HideAll() {
        for(int i = 0; i < createdBoxes.Length; i++) {
            createdBoxes[i].gameObject.SetActive(false);
        }
    }

    void ShowAll() {
        for(int i = 0; i < createdBoxes.Length; i++) {
            createdBoxes[i].gameObject.SetActive(true);
        }
    }


}
