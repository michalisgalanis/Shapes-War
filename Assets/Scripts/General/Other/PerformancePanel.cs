using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class PerformancePanel : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private int currentFps;
    private List<int> fpss;

    private void Awake() {
        fpss = new List<int>();
        fpsText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        currentFps = ((int)(1f / Time.unscaledDeltaTime));
        if (currentFps != 0) fpss.Add(currentFps);
        int avgFps, sum = 0;
        for (int i = 0; i < fpss.Count; i++) {
            sum += fpss[i];
        }
        if (fpss.Count > 2) {
            avgFps = sum / fpss.Count;
            fpsText.text = "C: " + currentFps + "/A:" + avgFps;
        }
    }

    public void ClearAvgFps() {
        if (fpss == null) fpss = new List<int>();
        else fpss.Clear();
    }
}
