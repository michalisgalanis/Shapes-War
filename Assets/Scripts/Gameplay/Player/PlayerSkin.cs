using UnityEngine;

public class PlayerSkin : MonoBehaviour {

    //References
    private Referencer rf;

    //Setup Variables
    public Utility.Gameplay.Player.skinTypes skinType;
    private GameObject playerSkin;

    //Runtime Variables
    private int rotation = 0;
    private float speed = 1.5f;

    private void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        switch (skinType) {
            case Utility.Gameplay.Player.skinTypes.BOX_3D:
            playerSkin = Instantiate(rf.playerSkins[0], transform);
            break;
            case Utility.Gameplay.Player.skinTypes.CIRCLES:
            playerSkin = Instantiate(rf.playerSkins[1], transform);
            break;
        }
        UpdateEnabled();
    }

    public void UpdateEnabled() {
        int playerLevel = RuntimeSpecs.playerLevel;
        rotation = 0;
        if (playerLevel < 75) {
            ToggleSkinParts(0, 0, 0);
        } else if (playerLevel < 100) {
            ToggleSkinParts(1, 0, 0);
        } else if (playerLevel < 125) {
            ToggleSkinParts(3, 0, 0);
        } else if (playerLevel < 150) {
            ToggleSkinParts(5, 0, 0);
        } else if (playerLevel < 175) {
            ToggleSkinParts(5, 3, 0);
        } else if (playerLevel < 200) {
            ToggleSkinParts(5, 5, 1);
        } else if (playerLevel < 225) {
            ToggleSkinParts(5, 5, 3);
            rotation = 1;
        } else{
            ToggleSkinParts(5, 5, 5);
            rotation = 5;
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < playerSkin.transform.childCount; i++) {
            if (i < rotation) {
                playerSkin.transform.GetChild(i).Rotate(0, 0, 45 * Time.fixedDeltaTime * speed);
                playerSkin.transform.GetChild(i).GetChild(0).Rotate(0, 0, -90 * Time.fixedDeltaTime * speed);
            }
        }
    }

    private void ToggleSkinParts(int parts, int depth, int particles) {
        for (int i = 0; i < playerSkin.transform.childCount; i++) {
            if (i < parts)
                playerSkin.transform.GetChild(i).gameObject.SetActive(true);
            else
                playerSkin.transform.GetChild(i).gameObject.SetActive(false);
            if (i < depth)
                playerSkin.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            else
                playerSkin.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            if (i < particles)
                playerSkin.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
            else
                playerSkin.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
        }
    }
}
