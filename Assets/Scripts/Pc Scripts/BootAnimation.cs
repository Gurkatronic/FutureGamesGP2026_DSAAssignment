using UnityEngine;
using UnityEngine.UI;

public class BootAnimation : MonoBehaviour
{

    public float width = 1;
    public float height = 0;

    public Material material;
    public Image image;
    public bool active = false;

    private void Awake() {

    }
    private void Update() {
        if(active && material != null) {
            material.SetFloat("CircleWidth", width);
            material.SetFloat("CircleHeight", height);
        }
    }
}
