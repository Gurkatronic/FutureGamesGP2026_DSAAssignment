using UnityEngine;

public class AnimationSoundPlay : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlaySound(SoundClipType clipType) {
        audioSource.PlayOneShot(SoundManager.Instance.GetObjectSound(clipType));
    }
}
