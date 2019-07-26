using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public GameObject[] scoreText;
    private int currentScore;

    void Start(){
        currentScore = 0;
    }


    void Update(){
        for (int i = 0; i < scoreText.Length; i++){
            scoreText[i].GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
        }
    }

    public void addPoints(int points){
        currentScore += points;
    }
}
