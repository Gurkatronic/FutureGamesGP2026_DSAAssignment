using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
    [SerializeField] TMP_InputField singleField;
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

        Stopwatch stopwatch = new();
        stopwatch.Start(); //<---------------------------------------------------------------------------start
        (int low, int high) = searchRangeTask.Execute(sortedArrayIndex, tenantsData.allTenants.ToArray(), lowBound, highBound2, sortMethod);
        stopwatch.Stop();//<-----------------------------------------------------------------------------stop
        //float timerAfter = Time.time;
        int[] sortedValueRange = sortedArrayIndex[low..high];
        Debug.Log("Time spent searching range: " + stopwatch.ElapsedMilliseconds + " ms");
        Debug.Log("Time spent searching range: " + stopwatch.ElapsedTicks + " ticks");

        ShowIndexBased(sortedValueRange);
    }

    [ContextMenu("Test linearSearch")]
    public void TestMethod() {
        //"2234-08-07T10:42:00"
        string searchString = singleField.text;
        TenantSortMethod sortMethod = TenantSortMethod.ExitDate;
        (DateTime lowBound, DateTime highBound) = dateTimeExtension.FormatDate(searchString);
        Stopwatch lineStop = new();
        int[] sortedIndex = sortedDict[sortMethod];
        lineStop.Start(); //<---------------------------------------------------------------------------start
        searchRangeTask.LinSearch(sortedIndex, tenantsData.allTenants.ToArray(), lowBound, sortMethod);
        lineStop.Stop(); //<-----------------------------------------------------------------------------stop
        Debug.Log("Time spent searching linear: " + lineStop.ElapsedMilliseconds + " ms");
        Debug.Log("Time spent searching linear: " + lineStop.ElapsedTicks + " ticks");

    }

    void ShowIndexBased(int[] index) {
        HideAll();
        for(int i = 0; i < index.Length; i++) {
            createdBoxes[i].ShowData(tenantsData.allTenants[index[i]]);
        }
        //Debug.Log("Showing " + index.Length + " elements");
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
