using Environment;
using UnityEngine;

public class ToiletInteract : MonoBehaviour, IInteractable {
    float rotationSpeed = 5;
    Vector3 rotateClose = Vector3.zero;
    Vector3 targetRotation = Vector3.zero;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    bool open = false;
    bool active = false;
    public void OnInteract() {
        Debug.Log("im toilet");
        open = !open;
        PlayClip();

        if(open)
            animator.SetTrigger("Open");
        else
            animator.SetTrigger("Close");
    }

    public void PlayClip() {
        audioSource.PlayOneShot(audioClip);
    }

}
