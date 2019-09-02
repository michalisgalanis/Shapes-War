using UnityEngine;

public class PlayerHead {
    //Setup Variables
    public Constants.Gameplay.Player.headTypes headType;

    //Runtime Variables
    public bool enabled;

    public PlayerHead(Constants.Gameplay.Player.headTypes headType) {
        this.headType = headType;
        enabled = false;
    }

    public void UpdateEnabled() {
        int tempPlayerLevel = RuntimeSpecs.playerLevel / 5;
        int playerLevel = Mathf.Max(tempPlayerLevel * 5, 1);

        switch (playerLevel) {
            case 1:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWC);
                break;
            case 5:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1);
                break;
            case 10:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1);
                break;
            case 15:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2);
                break;
            case 20:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2);
                break;
            case 25:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWC)
                    || (headType == Constants.Gameplay.Player.headTypes.RWC);
                break;
            case 30:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWC)
                    || (headType == Constants.Gameplay.Player.headTypes.RWC);
                break;
            case 35:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.LWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWR1);
                break;
            case 40:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.LWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWR1);
                break;
            case 45:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.LWC)
                    || (headType == Constants.Gameplay.Player.headTypes.LWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWC)
                    || (headType == Constants.Gameplay.Player.headTypes.RWR1);
                break;
            case 50:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.LWC)
                    || (headType == Constants.Gameplay.Player.headTypes.LWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWC)
                    || (headType == Constants.Gameplay.Player.headTypes.RWR1);
                break;
            case 75:
                enabled = (headType == Constants.Gameplay.Player.headTypes.CWL2)
                    || (headType == Constants.Gameplay.Player.headTypes.CWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWC)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.CWR2)
                    || (headType == Constants.Gameplay.Player.headTypes.LWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.LWC)
                    || (headType == Constants.Gameplay.Player.headTypes.LWR1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWL1)
                    || (headType == Constants.Gameplay.Player.headTypes.RWC)
                    || (headType == Constants.Gameplay.Player.headTypes.RWR1);
                break;
        }
    }
}
