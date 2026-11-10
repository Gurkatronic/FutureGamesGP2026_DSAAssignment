using Environment.Tasks;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "StructurePrepareTask", menuName = "Tasks/StructurePrepareTask")]
public class StructurePrepareTask : Task {

    public SortingTask sortingTask;
    public override void Execute() {
        throw new System.NotImplementedException();
    }

    public void Execute(TenantsData tenantsData) {
        Tenant[] convertedList = tenantsData.allTenants.ToArray();

        foreach(int i in Enum.GetValues(typeof(TenantSortMethod))) {
            TenantSortMethod sortMethod = (TenantSortMethod)i;
            int[] sortedIndexs = sortingTask.Execute(convertedList, sortMethod);
            tenantsData.sortedIndexDict.Add(sortMethod, sortedIndexs);

            //Then shove the values into a dictionary with sorted arrays
            Tenant[] sortedTenants = new Tenant[convertedList.Length];

            for(int j = 0; j < sortedTenants.Length; j++) {
                sortedTenants[j] = convertedList[sortedIndexs[j]];
            }
            tenantsData.sortedArrays.Add(sortMethod, sortedTenants);

            //now create the trees and add them into the designated dictionary
            BinaryTree<Tenant, TenantSortMethod> binaryTree = new(sortMethod);
            binaryTree.TakeRoot(sortedTenants);
            tenantsData.binaryTrees.Add(sortMethod, binaryTree);
        }

        Debug.Log($"Length of all dicts " +
            $"\n sortedindexdict: {tenantsData.sortedIndexDict.Count}" +
            $"\n sortedArrayDict: {tenantsData.sortedArrays.Count}" +
            $"\n binaryTreeDict: {tenantsData.binaryTrees.Count}");
        //sortingTask.Execute(tenantsData.allTenants.ToArray())
    }
}
