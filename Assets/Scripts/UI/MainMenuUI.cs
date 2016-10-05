using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    public Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText.text = "Score : " + GameSession.GetSession().GetScore(ConstMask.NAME_STAGE_1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DeleteAllScore()
    {
        GameSession.GetSession().DeleteAllScore();
    }
}
