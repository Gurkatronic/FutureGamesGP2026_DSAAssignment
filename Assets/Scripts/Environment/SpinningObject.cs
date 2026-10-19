using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    [SerializeField] Transform spinTransform;

    [SerializeField] public float spinSpeed;


    private void Update() {
        spinTransform.Rotate(new Vector3(0,1*spinSpeed*Time.deltaTime,0));
    }
}
