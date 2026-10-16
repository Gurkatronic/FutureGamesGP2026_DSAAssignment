using Environment;
using UnityEngine;
using System.IO;
using Assets.Scripts.Namespaces;

public class LoadComputer : MonoBehaviour, IInteractable
{
    [SerializeField]LoadTask loadTask;
    Kö<int> nyKö = new();
    Glist<int> nyGlist = new();

    void Awake() {
        
    }
    public void OnInteract() {
        
    }
}
