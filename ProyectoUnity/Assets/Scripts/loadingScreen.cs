using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadScene());
	}
	
    IEnumerator LoadScene()
    {
        var ao = SceneManager.LoadSceneAsync("game");
        yield return ao;
    }
}
