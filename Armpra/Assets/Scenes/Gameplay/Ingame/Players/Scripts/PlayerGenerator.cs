using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour {
    //References
    private Referencer rf;
    private Weapon weapon;


    //Setup Variables

    //Runtime Variables
    [HideInInspector] public List<PlayerHead> playerHeads;
    [HideInInspector] public float size;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        weapon = GetComponent<Weapon>();
        playerHeads = new List<PlayerHead>();

    }
    public void Start() {
        for (int i = 0; i < rf.playerHeads.Length; i++) {
            playerHeads.Add(new PlayerHead((Constants.Gameplay.Player.headTypes)Enum.GetValues(typeof(Constants.Gameplay.Player.headTypes)).GetValue(i)));
            rf.playerHeads[i].SetActive(false);
        }
        rf.playerWings[0].SetActive(true);
        rf.playerWings[1].SetActive(false);
        rf.playerWings[2].SetActive(false);
        rf.playerWings[3].SetActive(false);
        Refresh();
    }

    public void Refresh() {
        EstimateVars();
        UpdateVisuals();
    }

    private void EstimateVars() {
        size = getSizeAtLevel(RuntimeSpecs.playerLevel);
    }

    private void UpdateVisuals() {
        transform.localScale = new Vector3(size, size, size);
        //Update Heads
        for (int i = 0; i < playerHeads.ToArray().Length; i++) {
            playerHeads[i].UpdateEnabled();
            rf.playerHeads[i].SetActive(playerHeads[i].enabled);
        }
        //Update Wings
        if (RuntimeSpecs.playerLevel >= 25) {
            rf.playerWings[1].SetActive(true);
            rf.playerWings[2].SetActive(true);
            if (RuntimeSpecs.playerLevel >= 75)
                rf.playerWings[3].SetActive(true);
        }
        weapon.SetupFirepoints();
    }

    public static float getSizeAtLevel(int level) {
        return Constants.Functions.getPlayerSizeAtLevel(level);
    }
}
