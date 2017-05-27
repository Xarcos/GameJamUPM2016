using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadLeaderBoard: MonoBehaviour {
    [SerializeField]
    Text leaderBoardText;
    score[] leaderBoard;
	// Use this for initialization
	void Start () {
        leaderBoardText.text = "";
        leaderBoard = LeaderBoardManager.getOrderedScores();
        for (int i = 0; i < leaderBoard.Length; ++i) {
            leaderBoardText.text += "Player: "+leaderBoard[i].name + " - Score: " + leaderBoard[i].points +"\r\n";
        }
	}
}
