using System;
using UnityEngine;

public class TenantListView : MonoBehaviour
{
    [SerializeField] GameObject contentParent;
    [SerializeField] TenantDataBox tenantDataPrefab;

    TenantsData tenantsData;

    [SerializeField] TenantDataBox[] createdBoxes;
    [SerializeField] SortingTask sortingTask;



    public void LoadData(TenantsData data) {
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
            TenantSortMethod sortMethod = (TenantSortMethod)sortType;
            int[] sortedIndexes = sortingTask.Execute(tenantsData.allTenants.ToArray(), sortMethod);
            HideAll();

            ShowIndexBased(sortedIndexes);
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
