using Environment;
using UnityEngine;

public class SpinningLamp : MonoBehaviour, IInteractable
{
    [SerializeField] SpinningObject spinningObject;
    [SerializeField] float[] rotationSpeeds;
    int currentSpeed = 0;

    public void OnInteract() {
        currentSpeed++;
        currentSpeed = currentSpeed % rotationSpeeds.Length;
        SetSpeed();
    }

    private void Awake() {
        if(rotationSpeeds.Length <= 0) {
            Debug.Log("Speeds are not set on spinning lamp");

        }
        else
            SetSpeed();
    }

    void SetSpeed() {
        spinningObject.spinSpeed = rotationSpeeds[currentSpeed];
    }
}
