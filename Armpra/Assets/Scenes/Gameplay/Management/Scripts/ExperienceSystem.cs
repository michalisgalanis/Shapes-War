using TMPro;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour {
    public GameObject[] xpText;     //output texts
    public double currentXP;

    private void Start() {
        currentXP = 0;
    }

    private void Update() {
        for (int i = 0; i < xpText.Length; i++) {
            xpText[i].GetComponent<TextMeshProUGUI>().text = currentXP.ToString();
        }
    }
}
