using UnityEngine;

public class DynamicBackground : MonoBehaviour {
    //References
    private Referencer rf;

    //Constants
    private int shapesCounter;
    private float currentTimer;
    private const float spawnTimer = Constants.Timers.SHAPE_SPAWN_TIMER;
    private static float MIN_BORDER;
    private static float MAX_BORDER;

    private void Awake() {
        rf = GameObject.FindGameObjectWithTag(Constants.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        SpriteRenderer bgRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Constants.Gameplay.Background.MAP_HEIGHT = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit);
        Constants.Gameplay.Background.MAP_WIDTH = (bgRenderer.sprite.texture.width / bgRenderer.sprite.pixelsPerUnit);
    }

    private void Start() {
        MIN_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Constants.Gameplay.Background.MAP_HEIGHT * 0.5f;
        ChangeBackgroundColor();
        currentTimer = spawnTimer;
        shapesCounter = 0;
    }

    private void Update() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && shapesCounter < Constants.Gameplay.Background.SHAPES_AMOUNT) {
            //Selecting shape
            int shapeType = Random.Range(0, rf.backgroundShapes.Length);
            GameObject shape = rf.backgroundShapes[shapeType];
            //Generating Shape
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newShape = Instantiate(shape, temp, Quaternion.identity) as GameObject;
            newShape.transform.parent = rf.spawnedShapes.transform;
            //Customizing Color
            SpriteRenderer sr = shape.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = Random.Range(0.5f, 0.8f), v = Random.Range(0.5f, 0.7f), a = Random.Range(0.1f, 0.3f);
            float r = Color.HSVToRGB(h, s, v).r;
            float g = Color.HSVToRGB(h, s, v).g;
            float b = Color.HSVToRGB(h, s, v).b;
            sr.color = new Color(r, g, b, a);
            //Customizing Size
            float scaleRandom = Random.Range(0.4f, 0.4f + Constants.Gameplay.Background.SHAPES_SIZE_VAR);
            newShape.transform.localScale = new Vector3(scaleRandom, scaleRandom, 0);
            //Customiziing Rotation
            float rotationRandom = Random.Range(0f, 360f);
            newShape.transform.localRotation = Quaternion.Euler(0, 0, rotationRandom);
            //Resetting timer & counter
            shapesCounter++;
            currentTimer = 0f;
        }
    }

    public void ChangeBackgroundColor() {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        float hRand = Random.Range(0f, 1f), sRand = 1f, vRandBG = 0.1f; //vRandDE = 0.7f;
        sr.color = Color.HSVToRGB(hRand, sRand, vRandBG);
        //rf.pg.UpdateStripeVisuals(Color.HSVToRGB(hRand, sRand, vRandDE));
    }
}
