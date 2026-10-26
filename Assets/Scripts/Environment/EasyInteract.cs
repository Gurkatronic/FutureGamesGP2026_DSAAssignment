using UnityEngine;

public abstract class EasyInteract : MonoBehaviour
{
    bool isActive = false;
    public void SwitchInteract() {
        isActive = !isActive;

        if(isActive)
            ActiveMethod();
        else
            DeactiveMethod();
    }

    public virtual void ActiveMethod() {

    }

    public virtual void DeactiveMethod() {

    }
}
