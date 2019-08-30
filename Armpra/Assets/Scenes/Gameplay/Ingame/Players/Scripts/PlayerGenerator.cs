using UnityEngine;

public class PlayerGenerator : MonoBehaviour {
    //References
    private Weapon weapon;

    //Constants
    private const int MAX_VISUAL_CHANGES_LEVEL = Constants.Gameplay.Player.MAX_VISUAL_CHANGES_LEVEL;

    //Runtime Variables
    [HideInInspector] public float size;        //GH
    private int firepoints;                     //GH
    private int[,] firepointsEnabled;           //GH
    private int[] stripesEnabled;               //GH

    public void Awake() {
        weapon = GetComponent<Weapon>();
    }
    public void Start() {
        firepoints = transform.GetChild(0).transform.childCount;
        for (int i = 1; i < firepoints; i++) {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        firepointsEnabled = new int[MAX_VISUAL_CHANGES_LEVEL, 16] {    //0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5    //firepoints
                                                                        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},  //level 1
                                                                        {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},  //level 2
                                                                        {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},  //level 3* ^
                                                                        {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},  //level 4
                                                                        {0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},  //level 5
                                                                        {0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},  //level 6* /^\
                                                                        {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},  //level 7
                                                                        {0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},  //level 8
                                                                        {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},  //level 9
                                                                        {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},  //level 10* /^\ ^
                                                                        {1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},  //level 11
                                                                        {1,1,1,1,1,1,1,0,1,1,0,0,0,0,0,0},  //level 12
                                                                        {1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0},  //level 13
                                                                        {1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0},  //level 14* /^\/^\
                                                                        {1,1,1,1,1,1,1,0,1,1,1,1,0,0,0,0},  //level 15
                                                                        {1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0},  //level 16
                                                                        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0},  //level 17
                                                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},  //level 18
                                                                        {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1},  //level 19
                                                                        {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}}; //level 20

        stripesEnabled = new int[8] { 3, 6, 10, 14, 25, 50, 75, 100 };
    }

    public void Update() {
        EstimateVars();
        UpdateVisuals();
        weapon.SetupFirepoints();
    }

    private void EstimateVars() {
        size = getSizeAtLevel(RuntimeSpecs.playerLevel);
        firepoints = Mathf.RoundToInt(Mathf.Pow(Mathf.Clamp(RuntimeSpecs.playerLevel, 0f, MAX_VISUAL_CHANGES_LEVEL), 1.2f) * 0.3f);
    }

    private void UpdateVisuals() {
        transform.localScale = new Vector3(size, size, size);
        int[] tempFirepointsEnabled = new int[firepointsEnabled.GetUpperBound(1)];

        if (RuntimeSpecs.playerLevel < 1) {
            RuntimeSpecs.playerLevel = 1;
        } else if (RuntimeSpecs.playerLevel <= MAX_VISUAL_CHANGES_LEVEL) {
            for (int j = 0; j < firepointsEnabled.GetUpperBound(1); j++) { //for each type of firepoint
                tempFirepointsEnabled[j] = firepointsEnabled[RuntimeSpecs.playerLevel - 1, j];
                transform.GetChild(0).GetChild(j).gameObject.SetActive(((tempFirepointsEnabled[j] == 1)));
            }
        } else {
            for (int j = 0; j < firepointsEnabled.GetUpperBound(1); j++) {
                tempFirepointsEnabled[j] = firepointsEnabled[MAX_VISUAL_CHANGES_LEVEL - 1, j];
                transform.GetChild(0).GetChild(j).gameObject.SetActive(((tempFirepointsEnabled[j] == 1)));
            }
        }
        for (int i = 0; i < stripesEnabled.Length; i++) {
            if (RuntimeSpecs.playerLevel >= stripesEnabled[i]) {
                transform.Find("DesignElements").GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateStripeVisuals(Color stripeColor) {
        GameObject designElements = transform.Find("DesignElements").gameObject;
        for (int i = 0; i < designElements.transform.childCount; i++) {
            for (int j = 0; j < designElements.transform.GetChild(i).childCount; j++) {
                SpriteRenderer element = designElements.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>();
                element.color = stripeColor;
            }
        }
    }

    public static float getSizeAtLevel(int level) {
        return Constants.Functions.getPlayerSizeAtLevel(level);
    }
}
