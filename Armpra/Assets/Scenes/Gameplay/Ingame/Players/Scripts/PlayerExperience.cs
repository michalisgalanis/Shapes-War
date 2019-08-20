using UnityEngine;

public class PlayerExperience : MonoBehaviour {
    //Experience Stats
    public int playerLevel = 1;                //generated here
    public double currentPlayerXP = 0f;         //generated here
    private double nextXPMilestone;             //output
    private double prevXPMilestone;             //output
    private double xpBetweenMilestones;         //output
    public double remainingXP;                 //output
    private double accumulatedXP = 0f;          //output
    private float sizeAtLevel1;                 //input

    private ExperienceSystem es;                //output
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
        if (playerLevel == 1) sizeAtLevel1 = pg.size;
        es.currentXP = playerLevel; //output
        ps.playerLevel = playerLevel;   //output
        ps.XP = currentPlayerXP;        //output
        pg.currentLevel = playerLevel;
        prevXPMilestone = 200f * Mathf.Pow(playerLevel, 1.5f) - 100f;
        nextXPMilestone = 200f * Mathf.Pow(playerLevel + 1, 1.5f) - 100f;
        xpBetweenMilestones = nextXPMilestone - prevXPMilestone;
        //Customizing Progress Bar
        remainingXP = xpBetweenMilestones - currentPlayerXP;
        double xpPercentage = currentPlayerXP / xpBetweenMilestones;
        float maxWidth = progressBar.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float rightValue = Mathf.Abs(((float)xpPercentage * maxWidth * (-1))) - maxWidth;
        progressBar.GetComponent<RectTransform>().offsetMax = new Vector2(rightValue, 0);
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
        ParticleSystem lvlUpParticles = Instantiate(levelUpParticles, transform.position, Quaternion.identity);
        float sizeIncrease = pg.size / sizeAtLevel1; if (sizeIncrease == 0) sizeIncrease = 1;
        lvlUpParticles.transform.parent = gameObject.transform;
        Debug.Log(levelUpParticles.gameObject.transform.localScale);
        Vector3 initialScale = levelUpParticles.gameObject.transform.localScale;
        
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
