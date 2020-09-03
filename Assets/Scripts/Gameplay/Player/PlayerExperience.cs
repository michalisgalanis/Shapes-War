using TMPro;
using UnityEngine;
public class PlayerExperience : MonoBehaviour {
    //References
    private Referencer rf;

    private void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        RefreshXP();
    }

    private void LevelUp() {
        RuntimeSpecs.playerLevel++;
        RuntimeSpecs.levelsCollected++;
        RuntimeSpecs.accumulatedXP += RuntimeSpecs.currentPlayerXP;
        RuntimeSpecs.currentPlayerXP -= RuntimeSpecs.xpBetweenMilestones;
        Vector3 initialScale = rf.levelUpParticles.gameObject.transform.localScale;
        ParticleSystem lvlUpParticles = Instantiate(rf.levelUpParticles, transform.position, Quaternion.identity);
        float sizeIncrease = Mathf.Max(PlayerGenerator.getSizeAtLevel(RuntimeSpecs.playerLevel) / PlayerGenerator.getSizeAtLevel(1), 1);

        lvlUpParticles.transform.localScale = initialScale * sizeIncrease;
        lvlUpParticles.transform.parent = rf.spawnedParticles.transform;
        rf.ps.EstimateStats();
        rf.ps.RefillStats();
        rf.pg.UpdateVisuals();
        rf.wp.ResetShooting();
        UnlockSystem.CheckUnlockProgress();
        RefreshXP();
    }

    public void AddXP(double xp) {
        RuntimeSpecs.currentPlayerXP += xp;
        RuntimeSpecs.xpCollected += xp;
        RuntimeSpecs.totalXpGained += (int) xp;
        RefreshXP();
    }

    public void RefreshXP() {
        RuntimeSpecs.prevXPMilestone = Utility.Functions.ExperienceFunctions.getPrevXpMilestone(RuntimeSpecs.playerLevel);
        RuntimeSpecs.nextXPMilestone = Utility.Functions.ExperienceFunctions.getNextXpMilestone(RuntimeSpecs.playerLevel);
        RuntimeSpecs.remainingXP = RuntimeSpecs.xpBetweenMilestones - RuntimeSpecs.currentPlayerXP;
        RuntimeSpecs.xpBetweenMilestones = RuntimeSpecs.nextXPMilestone - RuntimeSpecs.prevXPMilestone;
        if (RuntimeSpecs.currentPlayerXP >= RuntimeSpecs.xpBetweenMilestones) {
            LevelUp();
        }
        //Customizing Progress Bar
        float xpPercentage = (float)(RuntimeSpecs.currentPlayerXP / RuntimeSpecs.xpBetweenMilestones);
        rf.progressBar.GetComponent<ProgressbarAnimation>().ApproachTarget(xpPercentage);
        //Outputting Texts
        foreach (TextMeshProUGUI text in rf.totalXPTexts) {
            text.text = Mathf.Round((float)RuntimeSpecs.currentPlayerXP).ToString();
        }
        foreach (TextMeshProUGUI text in rf.totalXPRemainingTexts) {
            text.text = Mathf.Round((float)RuntimeSpecs.xpBetweenMilestones).ToString();
        }
        foreach (TextMeshProUGUI text in rf.totalLevelTexts) {
            text.text = RuntimeSpecs.playerLevel.ToString();
        }
        foreach (TextMeshProUGUI text in rf.xpCollectedTexts) {
            text.text = "+" + RuntimeSpecs.xpCollected;
        }
        foreach (TextMeshProUGUI text in rf.levelsCollectedTexts) {
            text.text = "+" + RuntimeSpecs.levelsCollected;
        }
    }
}
