using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CodeReviewTask", menuName = "Tasks/CodeReviewTask")]
public class CodeReviewTask : Task
{
    Glist<int> glist = new();
    public bool massive = false;
    public int size = 5;
    public int lowNum = 1;
    public int highNum = 50;

    public override void Execute() {
        glist = new();
        if(massive) {
            for(int i = 0; i <= size; i++) {
                glist.Add(UnityEngine.Random.Range(lowNum, highNum));
            }
        }
        else {
            glist.Add(8);
            glist.Add(2);
            glist.Add(1);
            glist.Add(3);
            glist.Add(9);
        }


        PrintGlist();
        BubbleSorter(glist);
    }


    void BubbleSort(Glist<int> passedGlist, int index) {
        if(passedGlist.Count == 1)
            return;

        for(int i = index; i < passedGlist.Count - 1; i++) {
            Compare(passedGlist, i);
            BubbleSort(passedGlist, i+1);
        }


        Compare(passedGlist, 0);      
    }

    void BubbleSorter(Glist<int> passedGlist) {

        for(int i = 0; i < passedGlist.Count-1; i++) {
            for(int j = 0; j < passedGlist.Count-i-1; j++) {
                Compare(passedGlist, i);
                Debug.Log(i);
            }
        }

        Compare(passedGlist, 0);
    }

    void Compare(Glist<int> passedGlist, int index) {
        if(passedGlist[index+1] < passedGlist[index]) // if 1 < 0 swap 
            (passedGlist[index], passedGlist[index + 1]) = (passedGlist[index + 1], passedGlist[index]);

        PrintGlist(true);
    }

    void CompareFunction(Glist<int> passedGlist, int index) {
        for(int i = index+1; i < passedGlist.Count; i++) {
            for(int x = index; x < passedGlist.Count; x++) {
                if(passedGlist[x] < passedGlist[i]) {

                    (passedGlist[x], passedGlist[i]) = (passedGlist[i], passedGlist[x]);

                }
            }
            PrintGlist(true);
        }

        PrintGlist();
    }


    void PrintGlist(bool debug = false) {
        string printText = "";

        for(int i = 0; i < glist.Count; i++) {
            if(i == 0)
                printText += glist[i];
            else
                printText += " " + glist[i];
        }
        if(debug)
            Debug.Log("One compare iteration: " + printText);
        else 
            Debug.Log(printText);
    }



}
