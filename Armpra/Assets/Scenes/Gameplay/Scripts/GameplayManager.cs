using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start(){
        pauseMenu.gameObject.SetActive(false);
    }

    void Update(){
        
    }

    public void Die(){

    }
}
