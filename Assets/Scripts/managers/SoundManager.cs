using UnityEngine;
using System.Collections.Generic;
using System;


public enum SoundType {
    Step

}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Serializable]
    public class StepSound {

        [HideInInspector] public string name;
        public AudioClip[] audioClips;
    }

    public List<StepSound> stepSounds = new List<StepSound>();


    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public AudioClip GetStepSound(FloorType floorType) {
        int floor = (int)floorType;
        AudioClip output = null;
        if(stepSounds.Count >= floor) {
            int audioLen = stepSounds[floor].audioClips.Length;
            output = stepSounds[floor].audioClips[UnityEngine.Random.Range(0, audioLen)];
        }

        return output;
    }

    private void OnValidate() {
        for(int i = 0; i < stepSounds.Count; i++) {
           stepSounds[i].name = Enum.GetName(typeof(FloorType), i);
        }
    }
}
