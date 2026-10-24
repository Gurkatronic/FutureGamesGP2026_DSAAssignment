using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject computerFrame;
    [SerializeField] GameObject computerScreen;
    [SerializeField] Animator computerBoot;


    public void ShowPc(bool value = true) {
        computerFrame.SetActive(value);
        computerScreen.SetActive(value);
    }
}
