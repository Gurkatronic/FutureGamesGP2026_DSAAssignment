using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeasureStick : MonoBehaviour
{
    private enum StickState {
        inactive,
        fill
    }
    StickState currentState = StickState.inactive;
    public static float _MaxMeasureStickHeight = 350;
    public static float _LowFillSpeed = 50f;
    public static float _HighFillSpeed = 200f;
    float speed;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image imageComp;
    [SerializeField] TextMeshProUGUI nameTextBox;
    
    Vector2 targetSize = new Vector2(50,0);


    private void Awake() {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
        speed = UnityEngine.Random.Range(_LowFillSpeed, _HighFillSpeed);
    }

    private void Update() {
        switch(currentState) {
            case StickState.inactive:
                return;
            case StickState.fill:
                SmoothFill();
                break;
        }
    }

    public void FillStick(string stickName, float percentage, float value) {
        nameTextBox.text = stickName + " " + value.ToString();
        float newValue = Mathf.Lerp(0, 350, percentage);
        targetSize = new Vector2(rectTransform.sizeDelta.x, newValue);
        currentState = StickState.fill;
    }

    void SmoothFill() {
        rectTransform.sizeDelta = Vector2.MoveTowards(rectTransform.sizeDelta, targetSize, speed * Time.deltaTime);
        float delta = (rectTransform.sizeDelta - targetSize).magnitude;

        if(delta < 0.5f) {
            rectTransform.sizeDelta = targetSize;
            currentState = StickState.inactive;
        }
    }
}
