using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuButton:MonoBehaviour {

	public void OnLoadMenuClick() {
        SceneManager.LoadScene("mainmenu");
	}
}
