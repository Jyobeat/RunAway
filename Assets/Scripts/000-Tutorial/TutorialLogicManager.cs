using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLogicManager : MonoBehaviour
{

    public static TutorialLogicManager Instance;

    public GameObject _scene;

    public GameObject _ai;

    public Text _TxtTeaching;
    public int _teachingIndex = 3;

    public GameObject[] _knifes;
    public GameObject _hostage;

    private void Awake()
    {
        Instance = this;
    }
    //创造一个新手指导模式下的AI
    public void CreateAIForTutorial()
    {
        Invoke("InstantiateAi", 2f);
    }

    public void InstantiateAi()
    {
        _ai.GetComponent<AIAtkDmg>().StartPoint = new Vector3(0, 0, 3.05f);
        _ai.SetActive(true);
    }
    //显示小提示
    public void ShowTutorialTips()
    {
        if (_teachingIndex == 6)
        {
            InstantiateKnife();
            TerrainMoveManager.Instance._moveSpeed = 0;
        }
        if (_teachingIndex == 7)
        {
            TerrainMoveManager.Instance._moveSpeed = 1;
            InstantiateHostage();
        }
        Time.timeScale = 0;
        _TxtTeaching.transform.parent.gameObject.SetActive(true);
        _TxtTeaching.text = _Data._tutorialTips[_teachingIndex];
        _teachingIndex++;
    }
    //生成小刀
    private void InstantiateKnife()
    {
        foreach (var knife in _knifes)
        {
            knife.SetActive(true);
        }
    }
    //生成新手指导模式下的人质
    private void InstantiateHostage()
    {
        _hostage.SetActive(true);
        Invoke("TerrainStopMove", 2f);
    }
    //地形停止移动
    private void TerrainStopMove()
    {
        TerrainMoveManager.Instance._moveSpeed = 0;
    }

    public void WaitToShow()
    {
        Invoke("ShowTutorialTips", 5.5f);
    }
    //加载下一个游戏场景
    public void LoadGamePlayingScene()
    {
        
        _scene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.PlayScene;
        _scene.SetActive(true);
    }

}
