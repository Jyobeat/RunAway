using UnityEngine.UI;
using UnityEngine;

public class Btn_back : MonoBehaviour
{

    public GameObject _gamePauseWindows;
    public GameObject _loadScene;
    private GameObject _scoreText;
    private GameObject _coinText;

    private void Awake()
    {
        _scoreText = transform.GetChild(transform.childCount - 2).gameObject;
        _coinText = transform.GetChild(transform.childCount - 1).gameObject;
    }

    private void Start()
    {

        ShowScore();
        AddCoin();

    }

    /// <summary>
    /// 显示分数
    /// </summary>
    private void ShowScore()
    {
        if (_scoreText.name == "ScoreNum")
        {
            _scoreText.GetComponent<Text>().text = ((int)ScoreManager.Instance._score).ToString();
            //打破纪录
            if (ScoreManager.Instance._score > _Data._higestScore)
            {
                _Data._higestScore = (int)(ScoreManager.Instance._score);
                _Data.SaveData();
            }
        }

    }

    /// <summary>
    /// 增加金币
    /// </summary>
    private void AddCoin()
    {

        if (_coinText.name == "Txt_Coin")
        {
            int addCoin;
            if (ScoreManager.Instance._score > 50)
            {
                addCoin = (int)(ScoreManager.Instance._score * 0.25f);
                _coinText.GetComponent<Text>().text = "Coins+" + addCoin;
            }
            else
            {
                addCoin = 10;
                _coinText.GetComponent<Text>().text = "Coins+10";
            }

            _Data._coin += addCoin;
            _Data.SaveData();
        }
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        _gamePauseWindows.SetActive(true);
    }

    public void OnClickClose()
    {
        _gamePauseWindows.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickRestart()
    {
        _loadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.PlayScene;
        _loadScene.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnClickBack()
    {
        //GameLogicManager.Instance.DestroyPlayer();
        _loadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.MenuScene;
        _loadScene.SetActive(true);
        Time.timeScale = 1;      
    }

    public void OnClickResurrection()
    {
        PlayerCharacteristic.Instance.Resurrection();
        _gamePauseWindows.SetActive(false);
    }
}
