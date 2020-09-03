using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Referencer : MonoBehaviour {
    [Header("                                   [Camera & Background]")]
    public Camera cam;
    [HideInInspector] public CameraFollowPlayer camFollowPlayer;
    public GameObject background;
    public GameObject[] backgroundShapes;
    [HideInInspector] public DynamicBackground backgroundScript;

    [Header("                                       [GUI Panels]")]
    [Space(10)]
    public Canvas canvas;
    public GameObject gameplayHUD;
    public GameObject statsPanelUI;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;
    public GameObject lostMenuUI;
    public GameObject storeMenuUI;
    public GameObject debugPanelUI;
    public GameObject performancePanelUI;
    public GameObject hackPanelUI;
    public GameObject ammoPanelUI;
    public GameObject enemyDescriptorPanelUI;
    public GameObject unlockMenuUI;
    public Button[] ammoBulletButtons;
    public GameObject progressBar;
    public GameObject bossProgressBar;
    [Space(5)]
    public GameObject dualZoneJoystick;
    public GameObject normalJoystick;
    [Space(5)]
    public GameObject unlockItemPrefab;


    [Header("                                       [Store & Stats]")]
    [Space(10)]
    public TextMeshProUGUI[] totalCoinsTexts;
    public TextMeshProUGUI[] totalLevelTexts;
    public TextMeshProUGUI[] totalXPTexts;
    public TextMeshProUGUI[] totalXPRemainingTexts;
    public TextMeshProUGUI[] coinsCollectedTexts;
    public TextMeshProUGUI[] levelsCollectedTexts;
    public TextMeshProUGUI[] xpCollectedTexts;
    public TextMeshProUGUI bapText;
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI levelCompleteText;
    [Space(5)]
    public GameObject[] storeUpgrades;

    [Header("                                       [General Objects]")]
    [Space(10)]
    public GameObject player;
    public GameObject[] playerWings;
    public GameObject[] playerHeads;
    public GameObject[] playerSkins;
    public GameObject gameManager;
    public GameObject shield;
    public GameObject[] enemyTypes;
    public GameObject[] bossTypes;
    public GameObject[] bulletTypes;
    public GameObject[] powerupTypes;
    [HideInInspector] public List<GameObject> activePowerupTypes;
    public SpriteMask[] damageTypes;
    [Space(5)]
    public GameObject spawnedShapes;
    public GameObject spawnedEnemies;
    public GameObject spawnedProjectiles;
    public GameObject spawnedParticles;
    public GameObject spawnedPowerups;
    public GameObject spawnedOther;
    /*[Space(5)]*/
    [HideInInspector] public Transform enemyTarget;

    /*[Header("                                       [General Scripts]")]
    [Space(10)]*/
    [HideInInspector] public PlayerStats ps;
    [HideInInspector] public PlayerGenerator pg;
    [HideInInspector] public PlayerMovement pm;
    [HideInInspector] public PlayerExperience pe;
    [HideInInspector] public PlayerWeapon wp;
    [HideInInspector] public StoreSystem ss;
    [HideInInspector] public EnemySpawner es;
    [HideInInspector] public GameplayManager gm;
    [HideInInspector] public AmmoSystem asy;
    [HideInInspector] public PowerUpsSpawner pus;
    [HideInInspector] public Shield shieldScript;

    [Header("                                   [Particles & Shockwaves]")]
    [Space(10)]
    public ParticleSystem hitExplosionParticles;
    public ParticleSystem enemyDeathExplosionParticles;
    public ParticleSystem enemyDebris;
    public ParticleSystem bossDeathExplosionParticles;
    public ParticleSystem playerDeathExplosionParticles;
    public ParticleSystem shieldDestroyExplosionParticles;
    public ParticleSystem aoeExplosion;
    public ParticleSystem levelUpParticles;
    public ParticleSystem healingParticles;
    public GameObject shockwave;

    /*[Header("                                            [Audio]")]
    [Space(10)]*/
    [HideInInspector] public GameObject audioManager;
    [HideInInspector] public AudioManager audioManagerComp;

    [Header("                                          [Snapshots]")]
    [Space(10)]
    public Camera snapshotCamComp;
    public Sprite[] enemySprites;

    public void Awake() {
        cam = cam.GetComponent<Camera>();
        camFollowPlayer = cam.GetComponent<CameraFollowPlayer>();
        backgroundScript = background.GetComponent<DynamicBackground>();
        if (Utility.Audio.audioManager == null) {
            Utility.Audio.audioManager = Instantiate(audioManager, transform.parent);
            DontDestroyOnLoad(audioManager);
        }
        audioManager = Utility.Audio.audioManager;
        audioManagerComp = audioManager.GetComponent<AudioManager>();
        shieldScript = shield.GetComponent<Shield>();
        player = GameObject.FindGameObjectWithTag(Utility.Tags.PLAYER_TAG); //TODO FindRightPlayer
        ps = player.GetComponent<PlayerStats>();
        pm = player.GetComponent<PlayerMovement>();
        pe = player.GetComponent<PlayerExperience>();
        pg = player.GetComponent<PlayerGenerator>();
        wp = player.GetComponent<PlayerWeapon>();
        gameManager = gameObject;
        gm = gameManager.GetComponent<GameplayManager>();
        es = gameManager.GetComponent<EnemySpawner>();
        ss = gameManager.GetComponent<StoreSystem>();
        asy = gameManager.GetComponent<AmmoSystem>();
        pus = gameManager.GetComponent<PowerUpsSpawner>();
        enemyTarget = player.transform;
        activePowerupTypes = new List<GameObject>();
        //TakeSnapshots();
    }
    
    private void TakeSnapshots() {
        foreach (GameObject gameObject in bossTypes) {
            GameObject obj = Instantiate(gameObject);
            snapshotCamComp.orthographicSize = 0.33f * gameObject.transform.localScale.x;
            Utility.Functions.Utility.TakeObjectScreenShot(gameObject, "Boss Snapshots", snapshotCamComp);
            DestroyImmediate(obj);
        }
    }
}
