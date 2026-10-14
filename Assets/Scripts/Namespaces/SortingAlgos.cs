using System.Collections.Generic;
using UnityEngine;

namespace SortingAlgos
{
    public static class SortingAlgos {
        static bool firstRun = false;
        public static void QuickSortGeneric<T>(T[] array, int start, int end, IComparer<T> comparer) {

            if(end <= start) return;

            int pivot = PartionSearch<T>(array, start, end, firstRun, comparer);
            QuickSortGeneric<T>(array, start, pivot - 1, comparer);
            QuickSortGeneric<T>(array, pivot + 1, end, comparer);
        }

        static int PartionSearch<T>(T[] array, int start, int end, bool firstSearch, IComparer<T> comparer) {
            T pivot = array[end];

            if(firstSearch) {
                int rangeSearch = 3;
                int sortProcs = 1;
                bool pickedPivot = false;

                if(start + rangeSearch < end) {
                    for(int s = start; s < start + rangeSearch; s++) {
                        if(comparer.Compare(array[s], array[s + sortProcs]) < 0) {
                            sortProcs++;
                        }
                    }
                    if(sortProcs > rangeSearch) {
                        //the first part is sorted go pivot on the end
                        pivot = array[end];
                        pickedPivot = true;
                    }
                }
                sortProcs = 1;
                if(!pickedPivot) {
                    if(end - rangeSearch > start) {
                        for(int e = end; e < end-rangeSearch; e--) {
                            if(comparer.Compare(array[e], array[e-sortProcs])< 0) {
                                sortProcs++;
                            }
                        }
                        if(sortProcs > rangeSearch) {
                            //pick end here
                            pivot = array[start];
                            pickedPivot = true;
                        }
                    }
                }

                if(!pickedPivot) {
                    //pick middle pivot here
                    int middlePick = Mathf.RoundToInt(array.Length / 2);
                    pivot = array[middlePick];
                }

                firstRun = true;    
            }
            //if we use first search it should decide the first pivot point which should check what side is already sorted top 3 or bottom 3
            //can be fun to try if it works
            
            int i = start - 1;

            for(int x = start; x < end; x++ ) {
                if(comparer.Compare(array[x], pivot) < 0 ){
                    i++;
                    (array[x], array[i]) =
                    (array[i], array[x]);
                }
            }

            i++;
            (array[i], array[end]) =
            (array[end], array[i]);

            return i;
        }
    }
}
