using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour {
    public GameObject[] shapesPrefabs;
    private List<GameObject> shapes;

    public int numberOfShapes;
    public float sizeRange;

    //private int i;
    private int shapesCounter;
    private float currentTimer;
    private readonly float spawnTimer = 0.01f;

    //constants
    private static float MIN_BORDER = -19f;
    private static float MAX_BORDER = 19f;

    //private GameObject[] players;
    private PlayerGenerator playerGenerator;

    public void Start() {
        SpriteRenderer bgRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        MIN_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * (-0.5f);
        MAX_BORDER = (bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit) * 0.5f;
        /*players = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        while (players[i].GetComponent<PlayerGenerator>() == null)
            i++;*/
        playerGenerator = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameplayManager>().FindActualPlayer().GetComponent<PlayerGenerator>();
        shapes = new List<GameObject>();
        foreach (GameObject shape in shapesPrefabs) {
            shapes.Add(shape);
        }

        ChangeBackgroundColor();
        currentTimer = spawnTimer;
        shapesCounter = 0;

        //Ignore Collision
        Physics2D.IgnoreLayerCollision(8, 5);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(8, 12);
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(8, 14);
    }

    public void Update() {
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && shapesCounter < numberOfShapes) {
            //Selecting shape
            int shapeType = Random.Range(0, shapes.ToArray().Length);
            GameObject shape = shapes[shapeType];
            //Generating Shape
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newShape = Instantiate(shape, temp, Quaternion.identity);
            newShape.transform.parent = transform;
            //Customizing Color
            SpriteRenderer sr = shape.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = 0.75f, v = 0.6f, a = 0.3f;
            float r = Color.HSVToRGB(h, s, v).r;
            float g = Color.HSVToRGB(h, s, v).g;
            float b = Color.HSVToRGB(h, s, v).b;
            sr.color = new Color(r, g, b, a);
            //Customizing Size
            float scaleRandom = Random.Range(0.4f, 0.4f + sizeRange);
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
        float hRand = Random.Range(0f, 1f), sRand = 1f, vRandBG = 0.1f, vRandDE = 0.7f;
        sr.color = Color.HSVToRGB(hRand, sRand, vRandBG);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerGenerator.UpdateStripeVisuals(Color.HSVToRGB(hRand, sRand, vRandDE));
    }
}
