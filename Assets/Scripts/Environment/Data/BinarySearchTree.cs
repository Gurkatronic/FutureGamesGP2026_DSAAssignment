using System;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T, E> where T : IClassCompare<T, E> {
    //So this needs to hold the tenant and the value for comparison and the function so it understand how to do the comparisons
    E sortMethod;
    Node<T> root;
    public int Count = 0;

    public BinaryTree(E sortMethod) {
        this.sortMethod = sortMethod;
    }

    public T Find<D>(D searchValue) {
        T foundobject = default(T);

        Node<T> currentNode = root;

        while(currentNode != null) {
            int compareValue = currentNode.data.CompareData(searchValue, sortMethod);

            if(compareValue == 0) {
                foundobject = currentNode.data;
                break;
            }
            else if(compareValue < 0) 
                currentNode = currentNode.left;
            else
                currentNode = currentNode.right;
        }

        return foundobject;
    }

    public List<T>? FindRange<D>(D lowRange, D highRange) { //DFS search, dive hard to either edge to find values.
                                                            //So by going in order traversal here and checking left first i get the values in the right order
                                                            // 0->10
                                                            //collection to get the values
        List<T> foundObjects = new List<T>();
        if(root == null) {
            Console.WriteLine("Tree is empty take root first");
            return null;
        }

        Stack<Node<T>> stackNodes = new(); //we use a stack cause we want to check the top element first
        Node<T> currentNode = root;
        int nodesChecked = 0;

        while(currentNode != null || stackNodes.Count > 0) {

            while(currentNode != null) { //check here if we should go further left

                stackNodes.Push(currentNode); // save previous node so we can backtrack
                int compareLow = currentNode.data.CompareData(lowRange, sortMethod);
                nodesChecked++;

                if(compareLow > 0) { // if value node is 2005 and low is 2002    2005 > 2002 will be 1 go left
                    currentNode = currentNode.left;
                }
                else
                    break;
            }

            currentNode = stackNodes.Pop(); //get the node and check it
            if(currentNode != null) {

                int compareLow = currentNode.data.CompareData(lowRange, sortMethod);
                int compareHigh = currentNode.data.CompareData(highRange, sortMethod);
                nodesChecked += 2; //cause we do a double check here

                if(compareLow >= 0 && compareHigh <= 0) { //check if the node is within range here
                    foundObjects.Add(currentNode.data);
                }

                if(compareHigh < 0) { // do we need to go further right, 2005 node   2008 high range  will be -1 so we need to go furter to the right
                    currentNode = currentNode.right;
                }
                else
                    break;
            }
        }

        Console.WriteLine("Nodes checked: " + nodesChecked);
        return foundObjects;
    }

    public void InOrderTraversal() { // root > left until nothing then pop and start going right
        if(root == null) {
            Console.WriteLine("Tree is empty take root first");
            return;
        }

        Stack<Node<T>> stackNodes = new();
        Node<T> currentNode = root;
        int nodesVisited = 0;
        // need to dive farthest to the left first
        while(currentNode != null || stackNodes.Count > 0) {

            while(currentNode != null) {
                stackNodes.Push(currentNode);
                currentNode = currentNode.left;
            }
            currentNode = stackNodes.Pop();
            if(currentNode != null) {
                //currentNode.data.PrintData(sortMethod);
                nodesVisited++;
            }
            currentNode = currentNode.right;
        }
        Console.WriteLine($"Visited {nodesVisited} nodes");
    }

    public void PostOrderTraversal() { // root > Right until nothing then pop and start going left
        if(root == null) {
            Console.WriteLine("Tree is empty take root first");
            return;
        }

        Stack<Node<T>> stackNodes = new();
        Node<T> currentNode = root;
        int nodesVisited = 0;

        while(currentNode != null || stackNodes.Count > 0) {

            while(currentNode != null) {
                stackNodes.Push(currentNode);
                currentNode = currentNode.right;
            }

            currentNode = stackNodes.Pop();
            if(currentNode != null) {
                //currentNode.data.PrintData(sortMethod);
                nodesVisited++;
            }

            currentNode = currentNode.left;
        }

        Console.WriteLine($"Visited {nodesVisited} nodes");
    }

    public void GetLowestValue() {
        if(root == null) {
            Console.WriteLine("tree is empty or missing root. Fill tree first");
            return;
        }

        Node<T> currentNode = root;

        while(currentNode.left != null) {
            currentNode = currentNode.left;
        }

        if(currentNode != null) {
            //currentNode.data.PrintData(sortMethod);
        }
    }

    public void GetHighestValue() {
        if(root == null) {
            Console.WriteLine("tree is empty or missing root. Fill tree first");
            return;
        }

        Node<T> currentNode = root;

        while(currentNode.right != null) {
            currentNode = currentNode.right;
        }

        if(currentNode != null) {
            //currentNode.data.PrintData(sortMethod);
        }
    }

    public void TakeRoot(T[] dataArray) {
        int low = 0;
        int high = dataArray.Length - 1;

        TakeRootRecursive(dataArray, low, high);
    }

    void TakeRootRecursive(T[] dataArray, int low, int high) {
        if(low > high) return;

        int middle = (low + high) / 2;
        Branch(dataArray[middle]); // place the middle

        TakeRootRecursive(dataArray, low, middle - 1);
        TakeRootRecursive(dataArray, middle + 1, high);
    }

    public void Branch(T data) {
        Count++;
        if(root == null) {
            root = new(data);
            return;
        }

        Node<T> currentNode = root;

        while(currentNode != null) {
            int compareValue = data.CompareThis(currentNode.data, sortMethod);

            if(compareValue < 0) {
                if(currentNode.left == null) {
                    currentNode.left = new(data);
                    break;
                }
                else {
                    currentNode = currentNode.left;
                }
            }
            else {
                if(currentNode.right == null) {
                    currentNode.right = new(data);
                    break;
                }
                else
                    currentNode = currentNode.right;
            }
        }

    }
}

public class Node<T> {

    public T data;
    public Node<T>? left;
    public Node<T>? right;

    public Node(T data) {
        this.data = data;
    }

}
