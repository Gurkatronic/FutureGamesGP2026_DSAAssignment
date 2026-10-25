
using Environment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerObjects")]
    [SerializeField] GameObject flashLight;
    [Header("AudioSources")]
    [SerializeField] AudioSource footAudioSource;
    [Space(10)]
    [Header("Controls")]
    [SerializeField] CharacterController charController;
    [SerializeField] float gravityStr = 2f;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float rotateSpeed = 15;
    [SerializeField] float lookSensitivity = 2f;
    [SerializeField] float lookAngleLimit = 90;
    [SerializeField] float interactRange = 5f;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Camera mainCam;
    [SerializeField] bool isWalking = false;
    [Space(10)]

    [Header("Input actions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference interactAction;
    [SerializeField] LayerMask interactLayers;
    [Space(10)]

    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;
    float lookAngle = 0f;



    private void Awake() {

        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMove;
        moveAction.action.canceled += OnStop;
        lookAction.action.performed += OnLook;
        lookAction.action.canceled += OnLook;
        interactAction.action.performed += OnInteract;

    }

    public void DisableControls() {
        moveAction.action.Disable();
        //lookAction.action.Disable();
        interactAction.action.Disable();
    }

    public void ActivateControls() {
        moveAction.action.Enable();
        lookAction.action.Enable();
        interactAction.action.Enable();
    }

    void OnMove(InputAction.CallbackContext callbackContext) {
        moveInput = callbackContext.ReadValue<Vector2>();
        isWalking = true;
    }

    void OnStop(InputAction.CallbackContext callbackContext) {
        isWalking = false;
    }

    void OnInteract(InputAction.CallbackContext callbackContext) {
        //Send a raycast from the middle of the camera and interact with objects
        Debug.Log("Interact called");
        //RaycastHit hit;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.TransformDirection(transform.forward) * interactRange);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit[] rayFinder = Physics.RaycastAll(ray, interactRange, interactLayers);

        foreach(RaycastHit hit in rayFinder) {
            Debug.Log(hit.collider);
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if(interactable != null) {
                interactable.OnInteract();
                break;
            }
        }

        //if(Physics.Raycast(ray, out hit, interactRange)) {
        //    Debug.Log(hit.collider);
        //    IInteractable interactable = hit.collider.gameObject.GetComponentInParent<IInteractable>();
        //    if(interactable != null) {
        //        interactable.OnInteract();
        //    }
        //}
        
        
    }

    void OnLook(InputAction.CallbackContext callbackContext) {
        lookInput = callbackContext.ReadValue<Vector2>();
    }

    private void Update() {
        HandleMovement(moveInput);
        Vector2 mouseDelta = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());
        HandleLooking(mouseDelta);
        Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * interactRange, Color.blue);
        HandleFootSteps();

    }

    void HandleFootSteps() { //Får nog flytta den här till en annan script det blir så mycket utav den.
        if(isWalking) {
            if(footAudioSource.volume != 1) {
                float newValue = Mathf.Lerp(footAudioSource.volume, 1, Time.time);
                footAudioSource.volume = newValue;
            }
        }
        else {
            if(footAudioSource.volume != 0) {
                float newValue = Mathf.Lerp(footAudioSource.volume, 0, Time.time);
                footAudioSource.volume = newValue;
            }
            
        }
            
    }

    void HandleMovement(Vector2 moveVector) {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector2 newSpeed = new Vector2(moveVector.y * moveSpeed, moveVector.x * moveSpeed);

        Vector3 moveDirection = (forward *newSpeed.x) + (right * newSpeed.y);


        charController.Move(moveDirection * moveSpeed*Time.deltaTime);
        charController.Move(Vector3.down * gravityStr * Time.deltaTime);
    }

    void HandleLooking(Vector2 mouseDelta) {
        lookAngle += -mouseDelta.y * lookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -lookAngleLimit, lookAngleLimit);

        cameraTransform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        flashLight.transform.localRotation = Quaternion.Euler(0, 90, lookAngle);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * lookSensitivity, 0);
        
    }

    private void OnDisable() {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMove;
        moveAction.action.canceled -= OnStop;
        lookAction.action.performed -= OnLook;
        lookAction.action.canceled -= OnLook;
        interactAction.action.performed -= OnInteract;
    }
}
