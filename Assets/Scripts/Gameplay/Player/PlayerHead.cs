using UnityEngine;

public class PlayerHead {
    //Setup Variables
    public Utility.Gameplay.Player.headTypes headType;

    //Runtime Variables
    public bool enabled;

    public PlayerHead(Utility.Gameplay.Player.headTypes headType) {
        this.headType = headType;
        enabled = false;
    }

    public void UpdateEnabled() {
        int playerLevel = RuntimeSpecs.playerLevel;
        if (playerLevel < 5)
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWC);
        else if (playerLevel < 10) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1);
        } else if (playerLevel < 15) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1);
        } else if (playerLevel < 20) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2);
        } else if (playerLevel < 25) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2);
        } else if (playerLevel < 30) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWC)
                    || (headType == Utility.Gameplay.Player.headTypes.RWC);
        } else if (playerLevel < 35) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWC)
                    || (headType == Utility.Gameplay.Player.headTypes.RWC);
        } else if (playerLevel < 40) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.LWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWR1);
        } else if (playerLevel < 45) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.LWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWR1);
        } else if (playerLevel < 50) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.LWC)
                    || (headType == Utility.Gameplay.Player.headTypes.LWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWC)
                    || (headType == Utility.Gameplay.Player.headTypes.RWR1);
        } else if (playerLevel < 75) {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.LWC)
                    || (headType == Utility.Gameplay.Player.headTypes.LWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWC)
                    || (headType == Utility.Gameplay.Player.headTypes.RWR1);
        } else {
            enabled = (headType == Utility.Gameplay.Player.headTypes.CWL2)
                    || (headType == Utility.Gameplay.Player.headTypes.CWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWC)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.CWR2)
                    || (headType == Utility.Gameplay.Player.headTypes.LWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.LWC)
                    || (headType == Utility.Gameplay.Player.headTypes.LWR1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWL1)
                    || (headType == Utility.Gameplay.Player.headTypes.RWC)
                    || (headType == Utility.Gameplay.Player.headTypes.RWR1);
        }
    }
}
