using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    public void LoadScene() {
        SceneManager.LoadScene("Gameplay");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}
