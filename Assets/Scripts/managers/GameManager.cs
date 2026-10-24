using Environment;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Computer mainComputer;
    public PlayerController playerController;
    public UiController uiController;

    //pause game button
    //Return to main menu
    //Quit game

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
