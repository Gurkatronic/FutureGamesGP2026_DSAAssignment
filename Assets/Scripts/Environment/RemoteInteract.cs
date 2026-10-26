using Environment;
using UnityEngine;

public class RemoteInteract : MonoBehaviour, IInteractable
{
    [SerializeField] EasyInteract interactObject;

    public void OnInteract() {
        interactObject.SwitchInteract();
    }
}
