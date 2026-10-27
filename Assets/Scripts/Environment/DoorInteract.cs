using Environment;
using System.Collections;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable, IExtraInteractable {
    [Header("Animation")]
    string lockedBool = "Locked";
    string openedBool  = "Opened";
    string openDoorTrigger = "OpenDoor";
    [SerializeField] Animator animator;
    [SerializeField] AnimationSoundPlay animationSoundPlay;
    [Space(10)]

    [Header("Logic")]
    public bool isOpen = false;
    public bool isLocked = false;
    public bool hasKey = true;

    [SerializeField] GameObject keyObject;


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

    public void OnExtraInteract() {
        if(hasKey) { //Works but you can lock the door from the wrong side.
            Vector3 targetRotation = isLocked ? new Vector3(0, 0, 90) : new Vector3(0, 0, 0);
            keyObject.transform.localRotation = Quaternion.Euler(targetRotation);
            animationSoundPlay.PlaySound(SoundClipType.LockKey);
            ChangeLockState();
        }
    }

}
