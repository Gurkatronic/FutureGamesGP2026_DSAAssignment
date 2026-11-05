using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TenantListView : MonoBehaviour
{
    [SerializeField] GameObject contentParent;
    [SerializeField] TenantDataBox tenantDataPrefab;

    TenantsData tenantsData;

    [SerializeField] TenantDataBox[] createdBoxes;
    [SerializeField] SortingTask sortingTask;
    [SerializeField] SearchRangeTask searchRangeTask;

    Dictionary<TenantSortMethod, int[]> sortedDict = new();

    [SerializeField] TMP_InputField lowField;
    [SerializeField] TMP_InputField highField;
    DateTimeExtension dateTimeExtension;


    public void LoadData(TenantsData data) {
        sortedDict = new();
        dateTimeExtension = new();
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

    [ContextMenu("Test range search on exit")]
    public void TestingRangeSearch() {
        TenantSortMethod sortMethod = TenantSortMethod.ExitDate;
        DateTime lowTime = new(1970, 1, 01, 0,0,0);
        DateTime highTime = new(2234, 8, 4,11,36,58);
        int[] sortedArrayIndex = sortedDict[sortMethod];
        (int low, int high) = searchRangeTask.Execute(sortedArrayIndex, tenantsData.allTenants.ToArray(), lowTime, highTime, sortMethod);
        HideAll();
        int[] sortedValueRange = sortedArrayIndex[low..(high + 1)];

        ShowIndexBased(sortedValueRange);
    }

    public void SearchRange() {
        //maybe a way for it to read the string and see what is implied
        TenantSortMethod sortMethod = TenantSortMethod.ExitDate;
        int[] sortedArrayIndex = sortedDict[sortMethod];
        (DateTime lowBound, DateTime highBound) = dateTimeExtension.FormatDate(lowField.text);
        (DateTime lowBound2, DateTime highBound2) = dateTimeExtension.FormatDate(highField.text);

        Debug.Log("looping all elements according to sorted array index");
        for(int i = 0; i < sortedArrayIndex.Length; i++) {
            Debug.Log($"Index: {i} - {tenantsData.allTenants[sortedArrayIndex[i]].lastExitFromEngineRoom}");
        }

        (int low, int high) = searchRangeTask.Execute(sortedArrayIndex, tenantsData.allTenants.ToArray(), lowBound, highBound2, sortMethod);
        int[] sortedValueRange = sortedArrayIndex[low..high];
        Debug.Log($"SortedValue len: " + sortedValueRange.Length);
        Debug.Log($"len should be: {high-low} ");
        ShowIndexBased(sortedValueRange);
    }

    public void TestMethod(TenantSortMethod sortMethod) {

    }

    void ShowIndexBased(int[] index) {
        HideAll();
        for(int i = 0; i < index.Length; i++) {
            createdBoxes[i].ShowData(tenantsData.allTenants[index[i]]);
        }
        Debug.Log("Showing " + index.Length + " elements");
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
