using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void OnPlay_ButtonClick()
    {
        SceneManager.LoadScene("game");
    }

    public void OnCredits_ButtonClick()
    {
        SceneManager.LoadScene("credits");
    }

    public void OnLeaderboard_ButtonClick()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void OnQuit_ButtonClick()
    {
        Application.Quit();
    }
}
