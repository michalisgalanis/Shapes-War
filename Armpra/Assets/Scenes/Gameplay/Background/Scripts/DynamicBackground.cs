using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    public Camera camera;
    public GameObject backLayer;
    public GameObject[] shapes;

    public int numberOfShapes;
    public float sizeRange;

    private int shapesCounter;
    private float currentTimer;
    private float spawnTimer = 0.01f;
    private bool positionConflict;

    //constants
    private const float MIN_BORDER = -19f;
    private const float MAX_BORDER = 19f;
    private const float HORIZONTAL_CAMERA_OFFSET = 4f;
    private const float VERTICAL_CAMERA_OFFSET = 6f;

    void Start(){
        backLayer.SetActive(true);
        positionConflict = false;
        currentTimer = spawnTimer;
        shapesCounter = 0;
    }
    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && shapesCounter < numberOfShapes)
        {
            //Selecting shape
            int shapeType = Random.Range(0, shapes.Length);
            GameObject shape = shapes[shapeType];
            //Customizing Position
            positionConflict = false;
            //Generating Shape
            Vector3 temp = new Vector3(GenerateX(), GenerateY(), 0);
            GameObject newShape = Instantiate(shape, temp, Quaternion.identity);
            newShape.transform.parent = transform;
            //Customizing Color
            SpriteRenderer sr = shape.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = 0.75f, v = 0.6f, a = 0.3f;
            float r = Color.HSVToRGB(h, s, v).r;
            float g = Color.HSVToRGB(h, s, v).g;
            float b = Color.HSVToRGB(h, s, v).b;
            sr.color = new Color(r,g,b,a);
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

    public void ChangeBlackgroundColor()
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        float hRand = Random.Range(0f, 1f), sRand = 1f, vRand = 0.1f;
        sr.color = Color.HSVToRGB(hRand, sRand, vRand);
    }


    float GenerateX()
    {
        float temp_X = Random.Range(MIN_BORDER, MAX_BORDER);
        if (temp_X > camera.transform.position.x - HORIZONTAL_CAMERA_OFFSET && temp_X < camera.transform.position.x + HORIZONTAL_CAMERA_OFFSET)
            if (positionConflict)
                temp_X = GenerateX();
            else positionConflict = true;
        return temp_X;
    }

    float GenerateY()
    {
        float temp_Y = Random.Range(MIN_BORDER, MAX_BORDER);
        if (temp_Y > camera.transform.position.y - VERTICAL_CAMERA_OFFSET && temp_Y < camera.transform.position.y + VERTICAL_CAMERA_OFFSET)
            if (positionConflict)
                temp_Y = GenerateY();
            else positionConflict = true;
        return temp_Y;
    }
}
