using UnityEngine;

public class Debugger : MonoBehaviour
{
    //References
    private Referencer rf;

    //Setup Variables
    public bool debugPlayerStats;

    public void Awake() {
        rf = GetComponent<Referencer>();
    }


    void Update()
    {
        if (debugPlayerStats) Debug.Log(rf.ps.GetStatValueOf(Constants.Gameplay.Player.playerStatTypes.MAX_HEALTH));
    }
}
