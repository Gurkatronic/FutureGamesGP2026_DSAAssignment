using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MovableWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform rectTransform;
    bool isHeld = false;
    float moveSpeed = 10050f;
    [SerializeField] InputActionReference lookAction;

    Vector3 startPos = Vector3.zero;

    [SerializeField] Vector2 mousePos = Vector2.zero;
    public void OnPointerDown(PointerEventData eventData) {
        isHeld = true;
        
    }

    public void OnPointerUp(PointerEventData eventData) {
        isHeld = false;
    }

    private void Awake() {
        startPos = rectTransform.anchoredPosition;
        lookAction.action.performed += GetMousePos;
        lookAction.action.canceled += GetMousePos;
    }

    private void Update() {
        if(isHeld) {
            
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, rectTransform.anchoredPosition + mousePos, moveSpeed * Time.deltaTime);
        }
        //else
            //rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, startPos, moveSpeed * Time.deltaTime);
    }


    void GetMousePos(InputAction.CallbackContext callbackContext) {
        mousePos = lookAction.action.ReadValue<Vector2>();
        //mousePos = Camera.main.ScreenToViewportPoint(mousePos);
    }
}
