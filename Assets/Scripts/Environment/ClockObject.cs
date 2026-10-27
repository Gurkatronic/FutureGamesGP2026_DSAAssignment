using UnityEngine;

public class ClockObject : MonoBehaviour
{
    //Kinda works men borde gå att göra med bara en rotate och speed
    const float startAngle = 180;
    public int startHour = 12;
    public int startMin = 0;

    public float numberStep = 30f;
    public float minuteStep = 6f;

    public Transform minuteArm;
    public Transform hourArm;

    public float timeMultiplier = 10;

    public float currentTime = 0;
    public float lastMin = 0;
    public int currentMin = 0;
    //the arms should have a time multiplier so 24hrs will pass in 2hrs not figuring out the suspect in this time will result in gameover

    public void Awake() {
        int testHour = 3;
        float newAngle = 3 * 30;
        Debug.Log(newAngle+180);
        hourArm.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, newAngle + 180));
    }

    public void LateUpdate() {
        currentTime += Time.deltaTime * timeMultiplier;
        currentMin = Mathf.CeilToInt(currentTime)/60;

        if(currentMin > lastMin) {
            lastMin = currentMin;
            MoveMinuteArm();

            if(lastMin % 60 == 0)
                MoveHourArm();
        }
        
    }

    public void MoveMinuteArm() {
        Debug.Log("Minute passed");
        Vector3 step = new Vector3(0, 0, 6);
        minuteArm.transform.Rotate(step);
    }

    public void MoveHourArm() {
        Debug.Log("Minute passed");
        Vector3 step = new Vector3(0, 0, 30);
        hourArm.transform.Rotate(step);
    }

    //every 10 second move the hour arm 0.6 angles on the local Z axis
    //minute arm should move 0.6 angles every minute


    //Minute moves 1 number step per 5 min
}
