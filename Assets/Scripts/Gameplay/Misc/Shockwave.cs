using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour {
    public void Start() {
        StartCoroutine(wait());
    }

    private IEnumerator wait() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}