using UnityEngine;

public class StareKillerController : MonoBehaviour
{
    [SerializeField] string walkString;
    [SerializeField] string runString;
    [SerializeField] string stopString;
    [SerializeField] string idleString;

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

    public void StopAnimation() {
        animator.SetTrigger(idleString);
    }
}
