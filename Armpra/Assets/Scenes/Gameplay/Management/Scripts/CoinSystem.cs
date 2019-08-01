using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public GameObject[] coinsText;
    private int currentCoins;

    void Start(){
        currentCoins = 0;
    }

    void Update(){
        for (int i = 0; i < coinsText.Length; i++)
            coinsText[i].GetComponent<TextMeshProUGUI>().text = currentCoins.ToString();
        
    }

    public void addCoins(int coins){
        currentCoins += coins;
    }

    public void removeCoints(int coins)
    {
        currentCoins -= coins;
    }
}
