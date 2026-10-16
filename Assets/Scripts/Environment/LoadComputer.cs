using Environment;
using UnityEngine;
using System.IO;
using Assets.Scripts.Namespaces;

public class LoadComputer : MonoBehaviour, IInteractable
{
    [SerializeField]LoadTask loadTask;
    Kö<int> nyKö = new();

    void Awake() {

        nyKö = new();
        for(int i = 0; i < 10; i++) {
            nyKö.LineUp(i);
        }

        nyKö.RandomizeKö();
        Debug.Log("Kö size: " + nyKö.Count);
        int köCount = nyKö.Count;
        for(int i = 0; i < köCount; i++) {
            Debug.Log(nyKö.Enter());
        }
    }
    public void OnInteract() {
        
    }
}
