using UnityEngine;

public class MainMenuReferencer : MonoBehaviour
{
    [HideInInspector] public MainMenuManager mmg;
    public GameObject audioManagerPrefab;
    public GameObject background;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject eraseConfPanel;
    public GameObject statsMenu;
    [HideInInspector] public AudioManager audioManagerComponent;

    private void Awake() {
        mmg = GetComponent<MainMenuManager>();
        if (Utility.Audio.audioManager == null) {
            Utility.Audio.audioManager = Instantiate(audioManagerPrefab, transform.parent);
            DontDestroyOnLoad(Utility.Audio.audioManager);
        }
        audioManagerComponent = Utility.Audio.audioManager.GetComponent<AudioManager>();
    }
}
