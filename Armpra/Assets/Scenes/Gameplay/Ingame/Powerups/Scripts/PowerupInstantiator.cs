using UnityEngine;

public class PowerupInstantiator : MonoBehaviour {
    //Needed References
    public GameObject shieldPrefab;

    //Enums & InstantiationTypes
    public enum InstantiationType { Shield }
    public InstantiationType typeSelected;

    public void EnableEffect() {
        switch (typeSelected) {
            case InstantiationType.Shield:
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 initialScale = shieldPrefab.transform.localScale;
            float sizeIncrease = PlayerGenerator.getSizeAtLevel(player.GetComponent<PlayerStats>().playerLevel) / PlayerGenerator.getSizeAtLevel(1);
            if (sizeIncrease == 0) sizeIncrease = 1;
            GameObject shield = Instantiate(shieldPrefab, player.GetComponent<Transform>().localPosition, Quaternion.identity);
            shield.transform.localScale = initialScale * sizeIncrease;
            shield.transform.parent = player.transform;
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
