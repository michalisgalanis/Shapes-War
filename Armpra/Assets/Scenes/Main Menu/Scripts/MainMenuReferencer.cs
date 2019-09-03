using UnityEngine;

public class MainMenuReferencer : MonoBehaviour
{
    public GameObject audioManager;
    public AudioManager am;
    public MainMenuManager mmg;

    private void Awake() {
        mmg = GetComponent<MainMenuManager>();
    }
}
