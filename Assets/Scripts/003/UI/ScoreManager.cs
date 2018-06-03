using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScoreManager();
            return _instance;
        }
    }

    private ScoreManager() {
        _instance = this;
    }

    private Text _scoreTxt;
    public float _score;

	void Start () {
        _score = 0;
        _scoreTxt = GameObject.FindGameObjectWithTag(_Data._tagScoreTxt).GetComponent<Text>();
	}


    private void FixedUpdate()
    {
        if (!PlayerCharacteristic.Instance._isDead)
        {
            _score += 0.1f;
            _scoreTxt.text = ((int)_score).ToString();
        }
    }

    public void AddScoreByKillPawn()
    {
        _score += 100f;
    }

    public  void AddScoreByKillBoss()
    {
        _score += 500f;
    }

}
