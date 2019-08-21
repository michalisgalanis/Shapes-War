using UnityEngine;
using TMPro;
public class PlayerExperience : MonoBehaviour {
    //Experience Stats
    [HideInInspector] public int playerLevel = 1;                //generated here
    [HideInInspector] public double currentPlayerXP = 0f;         //generated here
    private double nextXPMilestone;             //output
    private double prevXPMilestone;             //output
    private double xpBetweenMilestones;         //output
    [HideInInspector] public double remainingXP;                 //output
    private double accumulatedXP = 0f;          //output

    private PlayerStats ps;                     //output
    private PlayerGenerator pg;                 //output

    public GameObject progressBar;
    public ParticleSystem levelUpParticles;
    public GameObject[] xpText;                 //output
    public GameObject[] levelText;              //output

    private void Start() {
        ps = GetComponent<PlayerStats>();
        pg = GetComponent<PlayerGenerator>();
    }

    private void Update() { 
        foreach (GameObject text in xpText) text.GetComponent<TextMeshProUGUI>().text = Mathf.Round((float)currentPlayerXP).ToString()+ " / " + Mathf.Round((float)xpBetweenMilestones).ToString();
        foreach (GameObject text in levelText) text.GetComponent<TextMeshProUGUI>().text = playerLevel.ToString();

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

        Vector3 initialScale = levelUpParticles.gameObject.transform.localScale;
        ParticleSystem lvlUpParticles = Instantiate(levelUpParticles, transform.position, Quaternion.identity);
        float sizeIncrease = PlayerGenerator.getSizeAtLevel(playerLevel) / PlayerGenerator.getSizeAtLevel(1); if (sizeIncrease == 0) sizeIncrease = 1;
        lvlUpParticles.transform.localScale = initialScale * sizeIncrease;
        
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
