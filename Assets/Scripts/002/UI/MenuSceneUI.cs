using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Together;
using System;

public class MenuSceneUI : MonoBehaviour
{
    //广告ID
    private string _adsID = "XRGrPvPRn9kmSX0KO5U";
    public GameObject _NoNet;

    public GameObject _LoadScene;

    public GameObject _MainWindow;
    public GameObject _CharcWindow;
    public GameObject _RankWindow;
    public GameObject _RechargeWindow;
    public GameObject _QuitWindow;

    public Text _TxtCoin;
    public Text _TxtGem;

    private void Awake()
    {
        _Data.LoadData();
        InitialAds();
    }

    void Start ()
    {
        ReflashCoinAndGem();
    }
	
	
	void Update () {
		
	}

    /// <summary>
    /// 初始化广告
    /// </summary>
    private void InitialAds()
    {
        TGSDK.Initialize();
        TGSDK.PreloadAd();
        TGSDK.AdRewardSuccessCallback = OnAdRewardSuccess;
        TGSDK.AdRewardFailedCallback = OnAdRewardFailed;
    }

    /// <summary>
    /// 当在充值界面点击了广告
    /// </summary>
    public void OnClickShowAds()
    {
        //广告成功加载
        if (TGSDK.CouldShowAd(_adsID))
        {

            TGSDK.ShowAd(_adsID);
        }

        //广告没被加载
        else
        {
            _NoNet.SetActive(true);
        }
    }

    /// <summary>
    /// 广告播放失败
    /// </summary>
    /// <param name="obj"></param>
    public void OnAdRewardFailed(string obj)
    {
        _NoNet.SetActive(true);
    }
    /// <summary>
    /// 广告播放成功
    /// </summary>
    /// <param name="obj"></param>
    public void OnAdRewardSuccess(string obj)
    {
        _Data._gem += 5;
        _Data.SaveData();
        ReflashCoinAndGem();
    }

    /// <summary>
    /// 刷新金币
    /// </summary>
    public void ReflashCoinAndGem()
    {
        _TxtCoin.text = _Data._coin.ToString();
        _TxtGem.text = _Data._gem.ToString();
    }


    /// <summary>
    /// 点击了新手教程
    /// </summary>
    public void OnClickTutorial()
    {
        _MainWindow.SetActive(false);
        _LoadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.Tutorial;
        _LoadScene.SetActive(true);
    }

    /// <summary>
    /// 点击了开始按钮
    /// </summary>
    public void OnClickStartGame()
    {
        if (_Data._isTaught)
        {
            _MainWindow.SetActive(false);
            _LoadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.PlayScene;
            _LoadScene.SetActive(true);
        }
        else
        {
            _MainWindow.SetActive(false);
            _LoadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.Tutorial;
            _LoadScene.SetActive(true);
            _Data._isTaught = true;
            _Data.SaveData();
        }
        
        //CharcManager.Instance.KeepThePlayer();
    }

    /// <summary>
    /// 点击了人物按钮
    /// </summary>
    public void OnClickCharc()
    {
        _MainWindow.SetActive(false);
        _CharcWindow.SetActive(true);
    }

    /// <summary>
    /// 点击了排行榜按钮
    /// </summary>
    public void OnClickRank()
    {
        _MainWindow.SetActive(false);
        _RankWindow.SetActive(true);
        _RankWindow.transform.GetChild(0).GetChild(3).GetComponentInChildren<Text>().text =
            _Data._higestScore.ToString();
    }

    /// <summary>
    /// 点击了充值按钮
    /// </summary>
    public void OnClickRecharge()
    {
        _MainWindow.SetActive(false);
        _RechargeWindow.SetActive(true);
    }

    /// <summary>
    /// 点击了退出按钮
    /// </summary>
    public void OnClickQuit()
    {
        _MainWindow.SetActive(false);
        _QuitWindow.SetActive(true);
    }

    /// <summary>
    /// 点击了关闭按钮
    /// </summary>
    public void OnClickClose()
    {
        _QuitWindow.SetActive(false);
        _RechargeWindow.SetActive(false);
        _RankWindow.SetActive(false);
        _CharcWindow.SetActive(false);
        _MainWindow.SetActive(true);
        CharcManager.Instance.JudgeWhileClosing();
    }

    /// <summary>
    /// 点击了购买按钮
    /// </summary>
    public void OnClickBuy()
    {
        CharcManager.Instance.BuyCharc();
        ReflashCoinAndGem();
    }

    /// <summary>
    /// 点击了“是”，退出游戏
    /// </summary>
    public void OnClickQuitGame()
    {
        Application.Quit();
    }

}
