using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
public enum StructureType {
    Array = 0,
    PresortedIndex = 1,
    List = 2,
    Tree = 3
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
    [SerializeField] Button singleSarchButton;
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
    [Space(10)]

    [Header("Data")]
    [SerializeField] TenantsData tenantsData;

    [SerializeField] MeasureStick testStick;

    private void Start() {
        PrepareFieldDropdown<TenantSortMethod>(fieldDropDown);
        PrepareFieldDropdown<StructureType>(structureDropDown);
        jsonLoadTask.Execute();
        tenantsData = jsonLoadTask.FetchDataList();

        structurePrepareTask.Execute(tenantsData);
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

    [ContextMenu("test stick")]
    public void TestMeasureStick() {
        testStick.FillStick("Linear", 1f, 23053f);
    }
}
