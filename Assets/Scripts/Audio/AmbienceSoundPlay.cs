using UnityEngine;

public enum AmbienceType {
    Swell,
    Sting,

}

public class AmbienceSoundPlay : MonoBehaviour
{
    //Fire sound based on strength and ambience type.
    //This is to controll the ambience of the game and fire it from different events.

    SoundManager soundManager;
    //Needs a ref to the SoundManager

    private void Awake() {
        soundManager = SoundManager.Instance;
    }
}
