using Environment.Tasks;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BinaryTreeTask", menuName = "Tasks/BinaryTreeTask")]
public class BinaryTreeTask : Task {
    public override void Execute() {
        throw new System.NotImplementedException();
    }

    public T Execute<T, R, E>(BinaryTree<T,E> binaryTree, R searchValue) where T : IClassCompare<T, E> {
        return binaryTree.Find(searchValue);
    }


    public List<T> ExecuteDFS<T, R, E>(BinaryTree<T, E> binaryTree, R searchLow, R searchHigh) where T : IClassCompare<T, E> {
        return binaryTree.FindRange(searchLow, searchHigh);
    }

    public List<T> ExecuteBFS<T,R,E>(BinaryTree<T,E> binaryTree, R searchLow, R searchHigh) where T : IClassCompare<T, E> {
        return binaryTree.FindRangeBFS(searchLow, searchHigh);
    }

}
