using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBackground : MonoBehaviour
{
    public Camera camera;
    public GameObject layerBack;
    public GameObject shape;

    public int numberOfShapes;
    public float speedOfShapes;

    private int shapesCounter;
    private float currentTimer;
    private float spawnTimer = 0.05f;
    private bool positionConflict;

    //constants
    private const float MIN_BORDER = -19;
    private const float MAX_BORDER = 19;
    private const float HORIZONTAL_CAMERA_OFFSET = 4;
    private const float VERTICAL_CAMERA_OFFSET = 6;

    void Start(){
        layerBack.SetActive(true);

        currentTimer = spawnTimer;
        shapesCounter = 0;
    }
    void Update(){
        currentTimer += Time.deltaTime;
        if (currentTimer >= spawnTimer && shapesCounter < numberOfShapes)
        {
            positionConflict = false;
            Vector3 temp = new Vector3(GenerateX(), GenerateY(), 0);
            GameObject newShape = Instantiate(shape, temp, Quaternion.identity);
            newShape.transform.parent = transform;

            //Customizing Color
            SpriteRenderer sr = shape.GetComponent<SpriteRenderer>();
            float h = Random.Range(0f, 360f) / 360f, s = 0.75f, v = 0.6f;
            sr.color = Color.HSVToRGB(h, s, v);

            //Resetting timer & counter
            shapesCounter++;
            currentTimer = 0f;
        }
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
