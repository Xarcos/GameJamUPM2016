using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void OnPlay_ButtonClick()
    {
        SceneManager.LoadScene("loading");
    }

    public void OnCredits_ButtonClick()
    {
        SceneManager.LoadScene("credits");
    }

    public void OnBacktoMainMenu_ButtonClick()
    {
        SceneManager.LoadScene("mainmenu");
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
