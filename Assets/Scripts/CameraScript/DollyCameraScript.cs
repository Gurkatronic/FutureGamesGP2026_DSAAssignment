using System;
using Unity.Cinemachine;
using UnityEngine;


public class DollyCameraScript : MonoBehaviour
{

    private enum CameraState {
        Activated,
        Return,
        Waiting
    }
    [SerializeField] CameraState currentState = CameraState.Waiting;
    public float speedMove = 0.2f;
    [SerializeField] int prioSet = 2;
    [SerializeField] CinemachineCamera cineCamera;
    [SerializeField] CinemachineSplineDolly dollyComponent;

    public Action destinationArrived;
    public Action resetArrived;
    public bool autoReturn;
    public float timeToReturn = 1f;
    float currentTime;

    private void Awake() {
        
    }

    private void Update() {
        switch(currentState) {
            case CameraState.Waiting:
                return;
            case CameraState.Activated:
                if(dollyComponent.CameraPosition > 0.99f) {
                    //dollyComponent.SplineSettings;
                    destinationArrived?.Invoke();
                    if(autoReturn) {
                        AutoReturn();
                    }
                    else {
                        currentState = CameraState.Waiting;
                    }
                }
                break;
            case CameraState.Return:
                if(dollyComponent.CameraPosition < 0.1f) {
                    cineCamera.Priority = 0;
                    resetArrived?.Invoke();
                    currentState = CameraState.Waiting;
                }
                break;
        }
    }

    public void MoveTowards() {
        cineCamera.Priority = prioSet;
        if(dollyComponent.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed fixedSpeed) {
            fixedSpeed.Speed = speedMove;
        }

        currentState = CameraState.Activated;
    }

    public void MoveBack() {
        if(dollyComponent.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed fixedSpeed) {
            fixedSpeed.Speed = -speedMove;
        }

        currentState = CameraState.Return;
    }

    void AutoReturn() {
        if(currentTime < timeToReturn) {
            currentTime += Time.deltaTime;
        }
        else {
            MoveBack();
            currentState = CameraState.Return;
            currentTime = 0;
        }
    }
}
