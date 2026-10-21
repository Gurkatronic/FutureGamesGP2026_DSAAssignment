using Environment.Tasks;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public enum SortMethod {
    QuickSort,
    BubbleSort
}

public class FieldPackage {
    public string variableName;
    public bool isPublic;
    public object variableValue;

    public FieldPackage(string variableName, bool isPublic, object variableValue) {
        this.variableName = variableName;
        this.isPublic = isPublic;
        this.variableValue = variableValue;
    }

    public Type GetVariableType() {
        
        return variableValue.GetType();
    }
}


//För att kolla hastighet gör en timestamp med datetime now, sen när den är klar gör en till ta sedan ut hur många millisecs i skillnad
[CreateAssetMenu(fileName = "SortTask", menuName = "Scriptable Objects/SortTask")]
public class SortTask : Task
{
    public SortType sortType = SortType.Firstname;


    public SortTask(SortType sortType) {
        this.sortType = sortType;
    }
    public override void Execute() {
        Debug.Log("this should not happen");
        throw new System.NotImplementedException();
    }
    public PersonClassList Execute(PersonClassList sortClass) {
        PersonClassList output = new();
        PersonClass personClass = sortClass.listOfPersons[0];
        FieldPackage[] personFields = GenericFieldNames<PersonClass>(personClass);

        foreach(FieldPackage fieldPackage in personFields) {
            Debug.Log("NAME OF VARIABLE IN PACKAGE: " + fieldPackage.variableName);
            Debug.Log("VALUE OF VARIABLE IN PACKAGE: " + fieldPackage.variableValue);
            Debug.Log("Type of variable: " + fieldPackage.variableValue.GetType());
        }
        GenericFieldNames<PersonClassList>(sortClass);

        //BubbleSort();

        Debug.Log("Sort Complete");
        return sortClass; //bara för att inte få en crash
    }

    void BubbleSort() { //exchange sort istället 
        string word1 = "harald";
        string word2 = "korvar";
        string word3 = "fiskar";
        string word4 = "apor";
        string word5 = "aska";
        string word6 = "kaska";
        string word7 = "aaa";
        string word8 = "uqra";
        string word9 = "haarald";

        string[] words = new string[] { word1, word2, word3, word4, word5, word6, word7, word8, word9 };


        for(int i = 0; i < words.Length; i++) {
            for(int x = words.Length-1; x > i; x--) {

                int wordDepth = Mathf.Min(words[i].Length, words[x].Length);
                for(int d = 0; d < wordDepth; d++) { 
                    if(words[i][d] < words[x][d]) {
                        Debug.Log("Word I is higher up then word x");
                        Debug.Log($"Word i: {words[i]} > Word x: {words[x]}");
                        break;
                    }

                    else if(words[i][d] > words[x][d]) {
                        Debug.Log("Word X is higher up then word I");
                        Debug.Log($" Word x: {words[x]} > Word i: {words[i]}");
                        FlipSpots(i, x, words);
                        break;
                    }
                }
            }
        }

        foreach(string word in words) {
            Debug.Log(word);
        }
    }
    //FieldPackage<V>[]
    FieldPackage[] GenericFieldNames<T>(T obj) { //detta kan ta vilken type som helst och kolla alla fields som finns i den.
        FieldInfo[] fieldInfos = obj.GetType().GetFields();
        FieldPackage[] fieldPackage = new FieldPackage[fieldInfos.Length];
        for(int i = 0; i < fieldInfos.Length; i++) { 
            Debug.Log("name of field: " + fieldInfos[i].Name); //variablen namn
            Debug.Log("is it public: " + fieldInfos[i].IsPublic); // is public
            Debug.Log("Value of the field: " + fieldInfos[i].GetValue(obj)); //detta ger det faktiska värdet
            Debug.Log("type of field: " + fieldInfos[i].GetValue(obj).GetType()); //detta ger vilken type fielden har
            fieldPackage[i] = new FieldPackage(fieldInfos[i].Name, fieldInfos[i].IsPublic, fieldInfos[i].GetValue(obj));
        }

        return fieldPackage;
    }

    void GenericTest(PersonClassList classList) {
        PersonClass[] newList = classList.listOfPersons;
        //newList[0].GetType().GetFields
    }
    void FlipSpots<T>(int fromPos, int targetPos, T[] wordArray) {
        T tempWord = wordArray[fromPos];
        wordArray[fromPos] = wordArray[targetPos];
        wordArray[targetPos] = tempWord;
    }

    void QuickSort() {
        //split in half until sorted
    }

    void InsertionSort<T>(T[] array, int sortPos) {
        FieldPackage[] TFields = GenericFieldNames(array[0]);
        //IComparable<TFields[0].variableValue.GetType()> comparable = default;
        //comparable.CompareTo("word", "word2");
        
        for(int i = 0; i < array.Length; i++) {
            FieldPackage[] iFields = GenericFieldNames(array[i]);

            for(int j = array.Length-1; j >= 0; j--) {

                FieldPackage[] jField = GenericFieldNames(array[j]);

            }
        }

    }
}
