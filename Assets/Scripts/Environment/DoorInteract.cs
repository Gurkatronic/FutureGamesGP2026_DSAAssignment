using Environment;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable {
    string lockedBool = "Locked";
    string openedBool  = "Opened";
    string openDoorTrigger = "OpenDoor";
    [SerializeField] Transform handle;
    [SerializeField] float handleOpenAngle;
    [SerializeField] Animator animator;
    public bool isOpen = false;
    public bool isLocked = false;

    private void Awake() {
        if(animator == null) {
            gameObject.SetActive(false);
            Debug.LogWarning("Door is missing animator");
        }
        else {
            animator.SetBool(lockedBool, isLocked);
            animator.SetBool(openedBool, isOpen);
        }
    }

    public void OnInteract() {
        animator.SetTrigger(openDoorTrigger);
    }

    public void ChangeOpenState() {
        isOpen = !isOpen;
    }

    [ContextMenu("Lockdoor")]
    public void ChangeLockState() {
        isLocked = !isLocked;
        animator.SetBool(lockedBool, isLocked);
    }
}
