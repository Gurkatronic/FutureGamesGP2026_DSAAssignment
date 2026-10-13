using Environment;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Computer mainComputer;

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
