using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_ScoreNum : MonoBehaviour {

    private void Start()
    {
        if(ScoreManager.Instance._score>_Data._higestScore)
        {
            _Data._higestScore = (int)(ScoreManager.Instance._score);
            _Data.SaveData();
        }
    }

}
