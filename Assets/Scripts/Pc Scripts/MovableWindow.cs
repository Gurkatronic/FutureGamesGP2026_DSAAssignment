using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class MovableWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform rectTransform;
    bool isHeld = false;
    float moveSpeed = 15f;

    Vector3 startPos = Vector3.zero;
    public void OnPointerDown(PointerEventData eventData) {
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        isHeld = false;
    }

    private void Awake() {
        startPos = rectTransform.anchoredPosition;
    }

    private void Update() {
        if(isHeld)
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);
        else
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, startPos, moveSpeed * Time.deltaTime);
    }
}
