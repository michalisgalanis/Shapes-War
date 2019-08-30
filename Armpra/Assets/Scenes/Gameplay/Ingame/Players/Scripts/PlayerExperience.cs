using TMPro;
using UnityEngine;
public class PlayerExperience : MonoBehaviour {
    //References
    private Referencer rf;

    public void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        RuntimeSpecs.playerLevel = 1;
        RuntimeSpecs.mapLevel = 1;
        RuntimeSpecs.currentPlayerXP = 0;
        RuntimeSpecs.nextXPMilestone = 0;
        RuntimeSpecs.prevXPMilestone = 0;
        RuntimeSpecs.xpBetweenMilestones = 0;
        RuntimeSpecs.accumulatedXP = 0;
    }

    public void Update() {
        RuntimeSpecs.prevXPMilestone = Constants.Functions.getPrevXpMilestone(RuntimeSpecs.playerLevel);
        RuntimeSpecs.nextXPMilestone = Constants.Functions.getNextXpMilestone(RuntimeSpecs.playerLevel);
        RuntimeSpecs.remainingXP = RuntimeSpecs.xpBetweenMilestones - RuntimeSpecs.currentPlayerXP;
        RuntimeSpecs.xpBetweenMilestones = RuntimeSpecs.nextXPMilestone - RuntimeSpecs.prevXPMilestone;
        if (RuntimeSpecs.currentPlayerXP >= RuntimeSpecs.xpBetweenMilestones) {
            LevelUp();
        }
        //Customizing Progress Bar
        double xpPercentage = RuntimeSpecs.currentPlayerXP / RuntimeSpecs.xpBetweenMilestones;
        float maxWidth = rf.progressBar.transform.parent.gameObject.GetComponent<RectTransform>().rect.width;
        float rightValue = Mathf.Abs(((float)xpPercentage * maxWidth * (-1))) - maxWidth;
        rf.progressBar.GetComponent<RectTransform>().offsetMax = new Vector2(rightValue, 0);
        //Outputting Texts
        foreach (TextMeshProUGUI text in rf.xpTexts) {
            text.text = Mathf.Round((float)RuntimeSpecs.currentPlayerXP).ToString() + " / " + Mathf.Round((float)RuntimeSpecs.xpBetweenMilestones).ToString();
        }
        foreach (TextMeshProUGUI text in rf.levelTexts) {
            text.text = RuntimeSpecs.playerLevel.ToString();
        }
    }

    private void LevelUp() {
        RuntimeSpecs.playerLevel++;
        RuntimeSpecs.accumulatedXP += RuntimeSpecs.currentPlayerXP;
        RuntimeSpecs.currentPlayerXP -= RuntimeSpecs.xpBetweenMilestones;
        Vector3 initialScale = rf.levelUpParticles.gameObject.transform.localScale;
        ParticleSystem lvlUpParticles = Instantiate(rf.levelUpParticles, transform.position, Quaternion.identity);
        float sizeIncrease = PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1);
        if (sizeIncrease == 0) {
            sizeIncrease = 1;
        }

        lvlUpParticles.transform.localScale = initialScale * sizeIncrease;
        lvlUpParticles.transform.parent = rf.spawnedParticles.transform;
        rf.ps.RefillStats();
    }

    public void addXP(double xp) {
        RuntimeSpecs.currentPlayerXP += xp;
    }

    public void addXp(int xp) {
        RuntimeSpecs.currentPlayerXP += xp;
    }
}
