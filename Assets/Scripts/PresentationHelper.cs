using System.Collections;
using UnityEngine;

public class PresentationHelper : MonoBehaviour {

	void Start () {
        StartCoroutine(Hide());
	}

    private IEnumerator Hide() {
        yield return new WaitForSecondsRealtime(3.1f);
        gameObject.SetActive(false);
    }
	
}
