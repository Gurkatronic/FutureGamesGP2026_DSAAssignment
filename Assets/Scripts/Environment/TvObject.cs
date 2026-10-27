using System;
using UnityEngine;

public class TvObject : EasyInteract
{
    [SerializeField] Material tvStatic;
    [SerializeField] AudioSource audioSource;
    [SerializeField] string matRef = "_Alpha";
    [SerializeField] float activeValue = 1f;
    [SerializeField] float deactiveValue = 0f;

    public override void ActiveMethod() {
        tvStatic.SetFloat(matRef, activeValue);
        audioSource.volume = activeValue;
    }

    public override void DeactiveMethod() {
        tvStatic.SetFloat(matRef, deactiveValue);
        audioSource.volume = deactiveValue;
    }
}
