using UnityEngine;

public abstract class EasyInteract : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;

    private void Awake() {
        if(isActive)
            ActiveMethod();
        else
            DeactiveMethod();
    }
    public void SwitchInteract() {
        isActive = !isActive;

        if(isActive)
            ActiveMethod();
        else
            DeactiveMethod();
    }

    public abstract void ActiveMethod();

    public abstract void DeactiveMethod();
}
