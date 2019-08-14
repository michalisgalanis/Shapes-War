using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour {
    private void Start() {
        StartCoroutine(wait());
    }

    private IEnumerator wait() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


}
