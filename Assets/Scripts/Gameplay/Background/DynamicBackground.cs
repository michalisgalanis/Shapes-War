using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DynamicBackground : MonoBehaviour {
    //References
    private Referencer rf;

    //Constants
    private int shapesCounter;

    private static float MIN_BORDER;
    private static float MAX_BORDER;

    [HideInInspector] public List<GameObject> preWarmedShapes;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        SpriteRenderer bgRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Utility.Gameplay.Background.MAP_HEIGHT = bgRenderer.sprite.texture.height / bgRenderer.sprite.pixelsPerUnit;
        Utility.Gameplay.Background.MAP_WIDTH = bgRenderer.sprite.texture.width / bgRenderer.sprite.pixelsPerUnit;
    }

    void Start() {
        MIN_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * (-0.5f);
        MAX_BORDER = Utility.Gameplay.Background.MAP_HEIGHT * 0.5f;
        shapesCounter = 0;
    }


    private void PreWarmShapes(int count, bool bossLevel) {
        for (int i = 0; i < count; i++) {
            //Generating Shape
            Vector3 temp = new Vector3(Random.Range(MIN_BORDER, MAX_BORDER), Random.Range(MIN_BORDER, MAX_BORDER), 0);
            GameObject newShape = Instantiate(rf.backgroundShapes[Random.Range(0, rf.backgroundShapes.Length)], temp, Quaternion.identity);
            newShape.transform.parent = rf.spawnedShapes.transform;
            newShape.SetActive(false);
            preWarmedShapes.Add(newShape);
        }
    }

    private IEnumerator SpawnShapes(bool bossLevel) {
        rf.background.transform.GetChild(1).gameObject.SetActive(!bossLevel);
        ParticleSystem.MainModule main = rf.background.transform.GetChild(2).GetComponent<ParticleSystem>().main;
        ParticleSystem.ColorOverLifetimeModule colt = rf.background.transform.GetChild(2).GetComponent<ParticleSystem>().colorOverLifetime;
        main.startColor = Utility.Functions.GeneralFunctions.generateColor(0f, 0f, 1f, bossLevel ? 0.1f : 0.2f);
        colt.enabled = !bossLevel;
        PreWarmShapes(Utility.Gameplay.Background.SHAPES_AMOUNT, bossLevel);
        while (shapesCounter < Utility.Gameplay.Background.SHAPES_AMOUNT) {
            //Resetting timer & counter
            preWarmedShapes[shapesCounter].SetActive(true);
            preWarmedShapes[shapesCounter].GetComponent<Shape>().SetColor(bossLevel);
            shapesCounter++;
            yield return new WaitForSeconds(Utility.Gameplay.Background.SHAPE_SPAWN_TIMER);
        }
    }

    public void ChangeScenery(bool bossLevel) {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = (bossLevel ? Color.black : Utility.Functions.GeneralFunctions.generateColor(Random.Range(0f, 1f), 1f, 0.1f, 1f));
        foreach (GameObject shape in preWarmedShapes) {
            Destroy(shape);
        }
        preWarmedShapes.Clear();
        shapesCounter = 0;
        StartCoroutine(SpawnShapes(bossLevel));
    }
}
