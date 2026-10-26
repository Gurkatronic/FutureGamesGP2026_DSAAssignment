using UnityEngine;
using System.Collections.Generic;
using System;

public enum FloorType {
    Carpet,
    Wood,
    Concrete,
    Metal
}

public class Footstepper : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Vector2 randomPitch;
    [SerializeField] FloorType currentFloor = FloorType.Carpet;

    public void PlayStep() {
        DetectGround();
        source.PlayOneShot(SoundManager.Instance.GetStepSound(currentFloor));
    }

    void DetectGround() {
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit[] caster = Physics.RaycastAll(ray, 5f);

        foreach(RaycastHit hit in caster) {
            if(DecideFloor(hit.collider.tag))
                break;
        }
    }

    //Decide what floor based on the collider tag return true if currentfloor has found something new and change it
    bool DecideFloor(string tagName) {
        string[] floorNames = Enum.GetNames(typeof(FloorType));
        for(int i = 0; i < floorNames.Length; i++) {
            if(tagName == floorNames[i]) {
                currentFloor = (FloorType)i;
                return true;
            }
        }
        return false;

    }
}
