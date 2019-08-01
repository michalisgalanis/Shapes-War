using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceSystem : MonoBehaviour
{
    public GameObject[] xpText;     //output texts
    public double currentXP;

    void Start(){
        currentXP = 0;
    }


    void Update(){
        for (int i = 0; i < xpText.Length; i++){
            xpText[i].GetComponent<TextMeshProUGUI>().text = currentXP.ToString();
        }
    }

    public void addPoints(double points){
        currentXP += points;
    }
}
