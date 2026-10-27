using Environment;
using System;
using UnityEngine;

public class SpinningLamp : MonoBehaviour, IInteractable, IExtraInteractable
{
    [SerializeField] SpinningObject spinningObject;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Light lightSource;
    [SerializeField] float[] rotationSpeeds;
    [SerializeField] float[] pitchSpeeds;
    int currentSpeed = 0;
    int currentPitch = 0;

    public void OnExtraInteract() {
        throw new System.NotImplementedException();
    }

    public void OnInteract() {
        currentSpeed++;
        currentSpeed = currentSpeed % rotationSpeeds.Length;
        currentPitch++;
        currentPitch = currentPitch % pitchSpeeds.Length;
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
        audioSource.pitch = pitchSpeeds[currentPitch];
        DateTime dateTime = new(2002, 09, 10);

    }
}
