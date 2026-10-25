using UnityEngine;

public class StareKillerController : MonoBehaviour
{
    [SerializeField] string walkString;
    [SerializeField] string runString;
    [SerializeField] string stopString;

    [SerializeField] Animator animator;


    public void StartWalking() {
        animator.SetTrigger(walkString);
    }

    public void StopWalking() {
        animator.SetTrigger(stopString);
    }

    public void StartRunning() {
        animator.SetTrigger(runString);
    }
}
