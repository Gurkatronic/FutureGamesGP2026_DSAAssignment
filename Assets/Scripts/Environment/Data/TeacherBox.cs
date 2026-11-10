using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public enum StructureType {
    Array = 0,
    PresortedIndex = 1,
    List = 2,
}

public struct MeasureData {
    public string name;
    public float timeMs;
    public float timeTicks;

    public MeasureData(string name, float timeMs, float timeTicks) {
        this.name = name;
        this.timeMs = timeMs;
        this.timeTicks = timeTicks;
    }
}

public class TeacherBox : MonoBehaviour
{
    [Header("Defined settings")]
    [SerializeField] StructureType currentStructure = StructureType.Array;
    [SerializeField] TenantSortMethod currentSortMethod = TenantSortMethod.Name;

    [Space(10)]

    [Header("Single field")]
    [SerializeField] TMP_InputField singleInputField;
    TextMeshProUGUI singlePlaceHolder;
    [SerializeField] Button singleSearchButton;
    [Space(10)]

    [Header("Multiple fields")]
    [SerializeField] TMP_InputField lowRangeField;
    TextMeshProUGUI lowRangePlacerholder;
    [SerializeField] TMP_InputField highRangeField;
    TextMeshProUGUI highRangePlacerholder;
    [SerializeField] Button searchRangeButton;
    [Space(10)]

    [Header("Features")]
    [SerializeField] Button sortButton;
    [SerializeField] TMP_Dropdown structureDropDown;
    [SerializeField] TMP_Dropdown fieldDropDown;
    [Space(10)]

    [Header("Tasks")]
    [SerializeField] StructurePrepareTask structurePrepareTask;
    [SerializeField] JsonLoadTask jsonLoadTask;
    [SerializeField] SortingTask sortingTask;
    [SerializeField] SearchRangeTask searchRangeTask;
    [SerializeField] BinaryTreeTask binaryTreeTask;
    [SerializeField] LinearSearchTask linearSearchTask;
    [SerializeField] BinarySearchTask binarySearchTask;
    [Space(10)]

    [Header("Data")]
    [SerializeField] TenantsData tenantsData;

    [SerializeField] MeasureStick measureStickPrefab;
    [SerializeField] GameObject measureMSParent;
    [SerializeField] GameObject measureTICKParent;


    private void Update() {
        CheckTextFields();
    }

    void CheckTextFields() {
        singleSearchButton.interactable = singleInputField.text.Length > 0;
        searchRangeButton.interactable = (lowRangeField.text.Length > 0 && highRangeField.text.Length > 0);
    }

    private void Start() {
        PrepareFieldDropdown<TenantSortMethod>(fieldDropDown);
        PrepareFieldDropdown<StructureType>(structureDropDown);
        jsonLoadTask.Execute();
        tenantsData = jsonLoadTask.FetchDataList();

        structurePrepareTask.Execute(tenantsData);
    }

    public void SortButton() {

    }

    public void SingleSearch() { //testing for now
        List<MeasureData> measureDatas = new();
        string searchValue = singleInputField.text;

        
        List<Tenant> unsortedList = tenantsData.allTenants;
        List<Tenant> sortedList = tenantsData.sortedArrays[currentSortMethod].ToList();
        Tenant[] unsortedArray = tenantsData.allTenants.ToArray();
        Tenant[] sortedArray = tenantsData.sortedArrays[currentSortMethod];
        BinaryTree<Tenant, TenantSortMethod> binaryTree = tenantsData.binaryTrees[currentSortMethod];

        Stopwatch stopwatchLin = MeasureFunction(() => linearSearchTask.Execute(tenantsData.allTenants, searchValue, currentSortMethod));
        MeasureData measureDataLin = new("linearsearch", stopwatchLin.ElapsedMilliseconds, stopwatchLin.ElapsedTicks);
        measureDatas.Add(measureDataLin);

        Stopwatch stopwatchTree = MeasureFunction(() => binaryTreeTask.Execute(tenantsData.binaryTrees[currentSortMethod], searchValue));
        MeasureData measureDataTree = new("Tree search", stopwatchTree.ElapsedMilliseconds, stopwatchTree.ElapsedTicks);
        measureDatas.Add(measureDataTree);

        Stopwatch stopwatchBin = MeasureFunction(() => binarySearchTask.Execute(sortedArray, searchValue, currentSortMethod));
        MeasureData measureDataBin = new("Binary search", stopwatchBin.ElapsedMilliseconds, stopwatchBin.ElapsedTicks);
        measureDatas.Add(measureDataBin);

        ShowMeasurements(measureDatas);
    }

    Stopwatch MeasureFunction(Action action) {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        action.Invoke();
        stopwatch.Stop();
        return stopwatch;
    }

    void ShowMeasurements(List<MeasureData> measureDatas) {
        foreach(Transform child in measureMSParent.transform) {
            Destroy(child.gameObject);
        }

        foreach(Transform child in measureTICKParent.transform) {
            Destroy(child.gameObject);
        }


        foreach(MeasureData data in measureDatas) {
            MeasureStick newStickMS = Instantiate(measureStickPrefab, measureMSParent.transform);
            MeasureStick newStickTICK = Instantiate(measureStickPrefab, measureTICKParent.transform);

            newStickMS.FillStick(data.name, 1f, data.timeMs);
            newStickTICK.FillStick(data.name, 1f, data.timeTicks);
        }
    }

    void PrepareFieldDropdown<E>(TMP_Dropdown dropdown) where E: Enum{
        dropdown.ClearOptions();
        string[] names = Enum.GetNames(typeof(E));
        dropdown.AddOptions(names.ToList<string>());
    }

    public void ChangeStructure(int value) {
        currentStructure = (StructureType)value;
    }

    public void ChangeField(int value) {
        currentSortMethod = (TenantSortMethod)value;
    }

}
