using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    //Experience Stats
    private int playerLevel = 1;                //generated here
    public double currentPlayerXP = 0f;         //generated here
    private double nextXPMilestone;             //output
    private double prevXPMilestone;             //output
    private double xpBetweenMilestones;         //output
    private double remainingXP;                 //output
    private double accumulatedXP = 0f;          //output

    private ExperienceSystem es;                //input
    private PlayerStats ps;                     //output
    private PlayerGenerator pg;                 //output
    public GameObject progressBar;
    public ParticleSystem levelUpParticles;

    void Start(){
        es = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExperienceSystem>();
        ps = GetComponent<PlayerStats>();
        pg = GetComponent<PlayerGenerator>();
    }

    void Update(){
        es.currentXP = playerLevel; //output
        ps.playerLevel = playerLevel;   //output
        ps.XP = currentPlayerXP;        //output
        pg.currentLevel = playerLevel;  //output
        prevXPMilestone = 200f * Mathf.Pow(playerLevel, 1.5f) - 100f;
        nextXPMilestone = 200f * Mathf.Pow(playerLevel + 1, 1.5f) - 100f;
        xpBetweenMilestones = nextXPMilestone - prevXPMilestone;
        //Customizing Progress Bar
        remainingXP = xpBetweenMilestones - currentPlayerXP;
        double xpPercentage = currentPlayerXP / xpBetweenMilestones;
        float maxWidth = progressBar.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;

        //Debug.Log(xpPercentage);
        float rightValue = Mathf.Abs(((float)xpPercentage * maxWidth * (-1))) - maxWidth;
        //Debug.Log(rightValue);
        progressBar.GetComponent<RectTransform>().offsetMax = new Vector2(rightValue, 0);

        //Debug.Log("currentPlayerXP: " + currentPlayerXP);
        //Debug.Log("remainingXP: " + remainingXP);
        if (currentPlayerXP >= xpBetweenMilestones)
            LevelUp();
    }
    void LevelUp(){
        playerLevel++;
        accumulatedXP += currentPlayerXP;
        currentPlayerXP -= xpBetweenMilestones;
        //Debug.Log("nextXPMilestone: " + nextXPMilestone);
        //Debug.Log("prevXPMilestone: " + prevXPMilestone);
        //Debug.Log("currentPlayerXP: " + currentPlayerXP);
        //Debug.Log("Player Level " + playerLevel + ": " + remainingXP + " XP remaining to Level Up!");
        ParticleSystem lvlUpParticles = Instantiate(levelUpParticles, transform.position, Quaternion.identity);
        lvlUpParticles.transform.parent = gameObject.transform;
    }

    public void addXP(double xp)
    {
        currentPlayerXP += xp;
    }
}
