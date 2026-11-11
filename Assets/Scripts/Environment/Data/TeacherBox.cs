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

    public MeasureData(string name, Stopwatch stopwatch) {
        this.name = name;
        this.timeMs = stopwatch.ElapsedMilliseconds;
        this.timeTicks = stopwatch.ElapsedTicks;
    }
}

public class TeacherBox : MonoBehaviour
{
    [Header("Defined settings")]
    [SerializeField] StructureType currentStructure = StructureType.Array;
    [SerializeField] TenantSortMethod currentSortMethod = TenantSortMethod.Name;
    [SerializeField] float maxMs = 5f;
    [SerializeField] float maxTick = 50000f;

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
    DateTimeExtension dateTimeExtension = new();


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

    [ContextMenu("Test BFS")]
    public void TestBFS() {
        
        string lowDate = "2234-08-04";
        string highDate = "2234-08-05";
        (DateTime lowRange, DateTime highRangeX) = dateTimeExtension.FormatDate(lowDate);
        (DateTime lowRangeX, DateTime highRange) = dateTimeExtension.FormatDate(highDate);


        BinaryTree<Tenant, TenantSortMethod> binaryTree = tenantsData.binaryTrees[TenantSortMethod.EnterDate];
        List<Tenant> foundTenantsBFS = binaryTree.FindRangeBFS(lowRange, highRange);
        List<Tenant> foundTenantsDFS = binaryTree.FindRange(lowRange, highRange);

        Debug.Log("BreadthFS: " + string.Join("\n ", foundTenantsBFS.Select(item => item.lastEntranceInEngineRoom)));
        Debug.Log("DepthFS: " + string.Join("\n ", foundTenantsDFS.Select(item => item.lastEntranceInEngineRoom)));
    }

    public void RangeSearch<T>(T searchLow, T searchHigh) {
        List<MeasureData> measureDatas = new();
        //No unsorted data structures in this one
        Tenant[] referenceArray = tenantsData.allTenants.ToArray();
        List<Tenant> sortedList = tenantsData.sortedArrays[currentSortMethod].ToList();
        Tenant[] sortedArray = tenantsData.sortedArrays[currentSortMethod];
        int[] presortedIndex = tenantsData.sortedIndexDict[currentSortMethod];
        BinaryTree<Tenant, TenantSortMethod> binaryTree = tenantsData.binaryTrees[currentSortMethod];

        //binary range search
        int rangeLow = 0;
        int rangeHigh = 0;
        Stopwatch binRange = MeasureFunction(() => (rangeLow, rangeHigh) = searchRangeTask.Execute(presortedIndex, referenceArray, searchLow, searchHigh, currentSortMethod));
        MeasureData binRangeData = new("Binary range", binRange);
        measureDatas.Add(binRangeData);
        int[] resultIndex = presortedIndex[rangeLow..rangeHigh]; //have to do this because i return the presorted index spots
        List<Tenant> binRangeResults = new();
        for(int i = 0; i < resultIndex.Length; i++) {
            binRangeResults.Add(referenceArray[resultIndex[i]]);
        }
        Debug.Log(string.Join("Binary range: \n", binRangeResults.Select(info => info.PrintClass())));

        //BFS
        List<Tenant> bfsResults = null;
        Stopwatch bfsSearch = MeasureFunction(() => bfsResults = binaryTreeTask.ExecuteBFS(binaryTree, searchLow, searchHigh));
        MeasureData bfsData = new("BreadthFS", bfsSearch);
        measureDatas.Add(bfsData);
        Debug.Log(string.Join("BreadthFS: \n", bfsResults.Select(info => info.PrintClass())));
        //DFS
        List<Tenant> dfsResults = null;
        Stopwatch dfsSearch = MeasureFunction(() => dfsResults = binaryTreeTask.ExecuteDFS(binaryTree, searchLow, searchHigh));
        MeasureData dfsData = new("DepthFS", dfsSearch);
        measureDatas.Add(dfsData);
        Debug.Log(string.Join("DepthFS: \n", dfsResults.Select(info => info.PrintClass())));

        ShowMeasurements(measureDatas);
    }

    public void SortingMeasure() { 
        List<MeasureData> measureDatas = new();
        //this will measure when the list has been printed instead of sorting is completed to measure difference with a presorted index
        //Quick sort

        //Quick sort presorted index

        //

    }

    public void StartMultipleSearch() {
        string lowInput = lowRangeField.text;
        string highInput = highRangeField.text;

        switch(currentSortMethod) {
            case TenantSortMethod.AptNum:
                //Int
                if(int.TryParse(lowInput, out int searchIntLow) && int.TryParse(highInput, out int searchIntHigh)) {
                    RangeSearch(searchIntLow, searchIntHigh);
                }
                else {
                    Debug.Log("Input was formatted incorrectly try again");
                    lowRangeField.text = "";
                    highRangeField.text = "";
                }
                break;
            case TenantSortMethod.Name:
                //string
                RangeSearch(lowInput, highInput);
                break;
            case TenantSortMethod.ExitDate:
                //datetime using low value here because it will default correctly when searching single
                (DateTime lowDate, DateTime highDateX) = dateTimeExtension.FormatDate(lowInput);
                (DateTime lowDateX, DateTime highDate) = dateTimeExtension.FormatDate(highInput); //Using X on the variables that are not used
                RangeSearch(lowDate, highDate);
                break;
            case TenantSortMethod.EnterDate:
                //datetime
                (DateTime lowDate2, DateTime highDateX2) = dateTimeExtension.FormatDate(lowInput);
                (DateTime lowDateX2, DateTime highDate2) = dateTimeExtension.FormatDate(highInput);
                RangeSearch(lowDate2, highDate2); //dirty names here aswell im soooorry
                break;
        }
    }

    public void StartSingleSearch() { //This is the only good way i figured out to convert the input without having to box and unbox variable
        string singleInput = singleInputField.text;

        switch(currentSortMethod) {
            case TenantSortMethod.AptNum:
                //Int
                if(int.TryParse(singleInput, out int searchInt)) {
                    SingleSearch(searchInt);
                }
                else {
                    Debug.Log("Input was formatted incorrectly try again");
                    singleInputField.text = "";
                }
                break;
            case TenantSortMethod.Name:
                //string
                SingleSearch(singleInput);
                break;
            case TenantSortMethod.ExitDate:
                //datetime using low value here because it will default correctly when searching single
                (DateTime lowDate, DateTime highDate) = dateTimeExtension.FormatDate(singleInput);
                SingleSearch(lowDate);
                break;
            case TenantSortMethod.EnterDate:
                //datetime
                (DateTime lowDate2, DateTime highDate2) = dateTimeExtension.FormatDate(singleInput);
                SingleSearch(lowDate2); //this is a little dirty but its just for measuring
                break;
        }
    }

    public void SingleSearch<T>(T searchValue) { //testing for now
        List<MeasureData> measureDatas = new();
        //Some way to convert the string into the different datatypes is needed here.
        //and a guard clause to stop wrong input from being fired
        
        List<Tenant> unsortedList = tenantsData.allTenants;
        List<Tenant> sortedList = tenantsData.sortedArrays[currentSortMethod].ToList();
        Tenant[] unsortedArray = tenantsData.allTenants.ToArray();
        Tenant[] sortedArray = tenantsData.sortedArrays[currentSortMethod];
        BinaryTree<Tenant, TenantSortMethod> binaryTree = tenantsData.binaryTrees[currentSortMethod];

        Stopwatch stopwatchLin = MeasureFunction(() => linearSearchTask.Execute(tenantsData.allTenants, searchValue, currentSortMethod));
        MeasureData measureDataLin = new("linearsearch", stopwatchLin);
        measureDatas.Add(measureDataLin);

        Stopwatch stopwatchBin = MeasureFunction(() => binarySearchTask.Execute(sortedArray, searchValue, currentSortMethod));
        MeasureData measureDataBin = new("Binary search", stopwatchBin);
        measureDatas.Add(measureDataBin);

        Stopwatch stopwatchTree = MeasureFunction(() => binaryTreeTask.Execute(tenantsData.binaryTrees[currentSortMethod], searchValue));
        MeasureData measureDataTree = new("Tree search", stopwatchTree);
        measureDatas.Add(measureDataTree);

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
            float msPercentage = Mathf.Min(( data.timeMs/ maxMs), 1);
            float tickPercentage = Mathf.Min((data.timeTicks / maxTick ), 1);
            Debug.Log("ms percentage: " + msPercentage);
            Debug.Log("tick percentage: " + tickPercentage);

            newStickMS.FillStick(data.name, msPercentage, data.timeMs);
            newStickTICK.FillStick(data.name, tickPercentage, data.timeTicks);
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
