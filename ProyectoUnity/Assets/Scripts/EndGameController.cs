using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
public class EndGameController : MonoBehaviour {

    [SerializeField] UnityEngine.UI.Text m_scoreText;
    [SerializeField] UnityEngine.UI.InputField m_inputField;

    int randomNameIndex;
    public string[] dogNames;
    string score;

	// Use this for initialization
	void Start () {
        randomNameIndex = Random.Range(0, dogNames.Length);
        score = ScoreManager.SCORE.ToString();
	}

    void Update()
    {
        m_scoreText.text = "Your Score:\n" + ((m_inputField.text == "") ? dogNames[randomNameIndex] : m_inputField.text) + " - "  + score ;
    }

    public void OnContinue_ButtonClick()
    {
        LeaderBoardManager.saveNewScore(ScoreManager.SCORE, m_inputField.text);
        SceneManager.LoadScene("mainmenu");
    }
}
