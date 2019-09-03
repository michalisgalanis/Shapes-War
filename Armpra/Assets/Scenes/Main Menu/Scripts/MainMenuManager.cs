using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //References
    public MainMenuReferencer mmr;
    public GameObject audioManagerPrefab;

    private void Awake() {
        mmr = GetComponent<MainMenuReferencer>();
    }

    void Start()
    {
        if (Constants.Audio.audioManager == null) {
            Constants.Audio.audioManager = Instantiate(audioManagerPrefab, transform.parent);
            DontDestroyOnLoad(Constants.Audio.audioManager);
        }
    }

    public void Playgame() {
        SceneManager.LoadScene(Constants.Scenes.GAMEPLAY_SCENE_NAME);
        SceneManager.UnloadSceneAsync(Constants.Scenes.MAIN_MENU_SCENE_NAME);
    }

    public void DeleteProgress() {
        SavingSystem.getInstance().DeleteProgress();
    }
    public void SaveSoundData() {
        SavingSystem.getInstance().Save(Constants.Data.dataTypes.AUDIO_DATA);
    }
}
