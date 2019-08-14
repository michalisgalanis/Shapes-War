using UnityEngine;

public class PlayerExperience : MonoBehaviour {
    //Experience Stats
    private int playerLevel = 1;                //generated here
    public double currentPlayerXP = 0f;         //generated here
    private double nextXPMilestone;             //output
    private double prevXPMilestone;             //output
    private double xpBetweenMilestones;         //output
    private double remainingXP;                 //output
    private double accumulatedXP = 0f;          //output
    private float sizeIncrease;                 //input

    private ExperienceSystem es;                //input
    private PlayerStats ps;                     //output
    private PlayerGenerator pg;                 //output
    public GameObject progressBar;
    public ParticleSystem levelUpParticles;

    private void Start() {
        es = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExperienceSystem>();
        ps = GetComponent<PlayerStats>();
        pg = GetComponent<PlayerGenerator>();
    }

    private void Update() {
        es.currentXP = playerLevel; //output
        ps.playerLevel = playerLevel;   //output
        ps.XP = currentPlayerXP;        //output
        if (pg.currentLevel != playerLevel) {
            float prevSize = pg.size;
            pg.currentLevel = playerLevel;  //output
            pg.ForceUpdate();
            float currSize = pg.size;
            sizeIncrease = currSize / prevSize;
        }
        
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

    private void LevelUp() {
        playerLevel++;
        accumulatedXP += currentPlayerXP;
        currentPlayerXP -= xpBetweenMilestones;
        //Debug.Log("nextXPMilestone: " + nextXPMilestone);
        //Debug.Log("prevXPMilestone: " + prevXPMilestone);
        //Debug.Log("currentPlayerXP: " + currentPlayerXP);
        //Debug.Log("Player Level " + playerLevel + ": " + remainingXP + " XP remaining to Level Up!");
        //levelUpParticles.gameObject.transform.localScale *= sizeIncrease;
        ParticleSystem lvlUpParticles = Instantiate(levelUpParticles, transform.position, Quaternion.identity);
        Debug.Log("Size Increase: " + sizeIncrease);
        Debug.Log("New Local Scale of Object: " + lvlUpParticles.transform.localScale);
        Debug.Log("New Local Scale of Prefab: " + levelUpParticles.transform.localScale);
        lvlUpParticles.transform.parent = gameObject.transform;
        ps.playerLevel = playerLevel;   //output
        ps.ForceUpdate();
        ps.RefillStats();
    }

    public void addXP(double xp) {
        currentPlayerXP += xp;
    }

    public void addXp(int xp) {
        currentPlayerXP += xp;
    }
}
