using Environment;
using UnityEngine;

public class CameraLookInteract : MonoBehaviour, IInteractable
{
    [SerializeField] DollyCameraScript dollyCameraScript;
    public void OnInteract() {
        dollyCameraScript.MoveTowards();
    }

}
