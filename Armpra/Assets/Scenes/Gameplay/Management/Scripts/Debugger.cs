using UnityEngine;
using UnityEngine.Assertions;

public class Debugger : MonoBehaviour {
    //References
    private Referencer rf;

    //Setup Variables
    public bool debugPlayerStats;

    public void Awake() {
        rf = GetComponent<Referencer>();
    }


    void Update() {
        if (!debugPlayerStats) return;
        Assert.AreEqual(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.ATTACK_SPEED), rf.wp.shootingTime);
        Assert.AreEqual(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MOVEMENT_SPEED), rf.pm.velocityFactor);

    }
}
