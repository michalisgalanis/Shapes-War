using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Referencer : MonoBehaviour {
    [Header("                                   [Camera & Background]")]
    public GameObject cam;
    public Camera camScript;
    public CameraFollowPlayer camFollowPlayer;
    public GameObject background;
    public GameObject[] backgroundShapes;
    public DynamicBackground backgroundScript;
    public GameObject backLayer;
    public ParticleSystem backgroundParticles;

    [Header("                                       [GUI Panels]")]
    [Space(10)]
    public GameObject scoreCoinsUI;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject lostMenuUI;
    public GameObject storeMenuUI;
    public GameObject debugPanelUI;
    public GameObject hackPanelUI;
    public GameObject ammoPanelUI;
    public Button[] ammoBulletButtons;
    public GameObject progressBar;
    [Space(5)]
    public GameObject movementJoystickUI;
    public GameObject attackJoystickUI;
    public Transform movementJoystickUIInnerCircle;
    public Transform attackJoystickUIInnerCircle;


    [Header("                                       [Store & Stats]")]
    [Space(10)]
    public TextMeshProUGUI[] coinTexts;
    public TextMeshProUGUI[] levelTexts;
    public TextMeshProUGUI[] xpTexts;
    public TextMeshProUGUI bapText;
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI levelCompleteText;
    [Space(5)]
    public Button[] storeUpgradeButtons;
    public GameObject[] storeLevelTexts;

    [Header("                                       [General Objects]")]
    [Space(10)]
    public GameObject player;
    public GameObject[] playerWings;
    public GameObject[] playerHeads;
    public GameObject gameManager;
    public GameObject shield;
    public GameObject[] enemyTypes;
    public GameObject[] bulletTypes;
    public GameObject[] powerupTypes;
    [Space(5)]
    public GameObject spawnedShapes;
    public GameObject spawnedEnemies;
    public GameObject spawnedProjectiles;
    public GameObject spawnedParticles;

    [Header("                                       [General Scripts]")]
    [Space(10)]
    public PlayerStats ps;
    public PlayerGenerator pg;
    public PlayerMovement pm;
    public PlayerExperience pe;
    public Weapon wp;
    public StoreSystem ss;
    public EnemySpawner es;
    public GameplayManager gm;
    public CoinSystem cs;
    public LevelGeneration lg;
    public AmmoSystem asy;
    public Shield shieldScript;

    [Header("                                   [Particles & Shockwaves]")]
    [Space(10)]
    public ParticleSystem hitExplosionParticles;
    public ParticleSystem enemyDeathExplosionParticles;
    public ParticleSystem playerDeathExplosionParticles;
    public ParticleSystem shieldDestroyExplosionParticles;
    public ParticleSystem aoeExplosion;
    public ParticleSystem levelUpParticles;
    public ParticleSystem healingParticles;
    public GameObject shockwave;

    public void Awake() {
        camScript = cam.GetComponent<Camera>();
        camFollowPlayer = cam.GetComponent<CameraFollowPlayer>();
        backgroundScript = background.GetComponent<DynamicBackground>();
        shieldScript = shield.GetComponent<Shield>();
        player = GameObject.FindGameObjectWithTag(Constants.Tags.PLAYER_TAG); //TODO FindRightPlayer
        ps = player.GetComponent<PlayerStats>();
        pm = player.GetComponent<PlayerMovement>();
        pe = player.GetComponent<PlayerExperience>();
        pg = player.GetComponent<PlayerGenerator>();
        wp = player.GetComponent<Weapon>();
        gameManager = gameObject;
        gm = gameManager.GetComponent<GameplayManager>();
        es = gameManager.GetComponent<EnemySpawner>();
        ss = gameManager.GetComponent<StoreSystem>();
        cs = gameManager.GetComponent<CoinSystem>();
        lg = gameManager.GetComponent<LevelGeneration>();
        asy = gameManager.GetComponent<AmmoSystem>();
        movementJoystickUIInnerCircle = movementJoystickUI.transform.GetChild(2);
        attackJoystickUIInnerCircle = attackJoystickUI.transform.GetChild(2);
    }

    public void Start() {
        foreach (TextMeshProUGUI text in xpTexts) {
            text.fontStyle = FontStyles.Bold;
            text.enableAutoSizing = true;
        }
        foreach (TextMeshProUGUI text in coinTexts) {
            text.fontStyle = FontStyles.Bold;
            text.enableAutoSizing = true;
        }
        foreach (TextMeshProUGUI text in levelTexts) {
            text.fontStyle = FontStyles.Bold;
            text.enableAutoSizing = true;
        }
        bapText.fontStyle = FontStyles.Bold;
        bapText.enableAutoSizing = true;
        enemiesRemainingText.fontStyle = FontStyles.Bold;
        enemiesRemainingText.enableAutoSizing = true;
    }
}
