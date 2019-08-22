using UnityEngine;

public class PowerupInstantiator : MonoBehaviour {
    //Needed References
    public GameObject shield;

    //Enums & InstantiationTypes
    public enum InstantiationType { Shield }
    public InstantiationType typeSelected;

    public void EnableEffect() {
        switch (typeSelected) {
            case InstantiationType.Shield:
                GameObject player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().FindActualPlayer();
                Instantiate(shield, player.GetComponent<Transform>().localPosition, Quaternion.identity).transform.parent = player.transform;
                break;
        }
    }

    public void DisableEffect() {
        switch (typeSelected) {
            case InstantiationType.Shield:
                Destroy(GameObject.FindGameObjectWithTag("Shield"));
                break;
        }
    }

    public void ResetEffect() {
        switch (typeSelected) {
            case InstantiationType.Shield:
                GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>().RestoreShieldStats();
                break;
        }
    }

    public void DisplayEffectStats() {
        Debug.Log(typeSelected.ToString());
    }
}
