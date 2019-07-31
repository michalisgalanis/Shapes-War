using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    //Experience Stats
    public int playerLevel;              //input
    public double currentPlayerXP;       //input
    private double nextXPMilestone;      //output
    private double prevXPMilestone;      //output
    private double xpBetweenMilestones;  //output
    private double remainingXP;          //output
    private double accumulatedXP;        //output

    private ExperienceSystem ss;        //input
    private PlayerStats ps;             //output

    void Start(){
        playerLevel = 1;
        currentPlayerXP = 0f;
        accumulatedXP = 0f;
        EstimateXP();

        ss = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExperienceSystem>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        currentPlayerXP = ss.currentXP - accumulatedXP; //input
        ps.playerLevel = playerLevel;   //output
        ps.XP = currentPlayerXP;        //output
        if (currentPlayerXP >= nextXPMilestone)
            LevelUp();
    }
    void LevelUp()
    {
        playerLevel++;
        accumulatedXP += currentPlayerXP;
        EstimateXP();
    }

    void EstimateXP(){
        prevXPMilestone = 100f * Mathf.Pow(playerLevel, 3);
        nextXPMilestone = 100f * Mathf.Pow(playerLevel + 1, 3);
        xpBetweenMilestones = nextXPMilestone - prevXPMilestone;
        remainingXP = xpBetweenMilestones - currentPlayerXP;
    }

}
