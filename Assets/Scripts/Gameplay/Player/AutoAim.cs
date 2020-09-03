using UnityEngine;

public class AutoAim : MonoBehaviour
{
    //References
    private Referencer rf;
    private EnemySpawner es;

    //Runtime Variables
    private float rotationValue;

    void Awake() {
        rf = GameObject.FindGameObjectWithTag(Utility.Tags.GAME_MANAGER_TAG).GetComponent<Referencer>();
        es = rf.es;
    }

    private void Start() {
        //rotationValue = 0;
    }

    // Update is called once per frame
    void Update() {
        //Vector2 nearestEnemyVector = FindNearestEnemy().transform.position;
        //Vector2 playerEnemyVector = transform.position;
        //Vector2 aimDirection = playerEnemyVector - nearestEnemyVector;

        //FindNearestEnemy().transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().color = new Color()
        //transform.rotation = Quaternion.LookRotation(aimDirection);
    }


    private GameObject FindNearestEnemy() {
        float minDistance = int.MaxValue;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in es.preWarmedEnemies) {
            if (enemy.activeInHierarchy) {
                float tempDist = Vector2.Distance(rf.ps.transform.position, enemy.transform.position);
                if (tempDist < minDistance) {
                    minDistance = tempDist;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }
}
