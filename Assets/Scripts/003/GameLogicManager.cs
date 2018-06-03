using Together ;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;


public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance;

    private string _adsID = "XRGrPvPRn9kmSX0KO5U";

    private GameObject _player;
    private PlayerMovement _playerMovement;
    private CapsuleCollider _playerCollider;

    
    public GameObject _boss;
    //当前击杀小怪数量
    private int _currentKillCount;
    //生成boss需要击杀的小怪数量
    private int _needToKillCount;
    //现在是不是打boss的时间
    private bool _isBossTime;

    public bool IsBossTime
    {
        get { return _isBossTime; }
        set { _isBossTime = value; }
    }
    


    //所有ai预制体
    public GameObject[] _aiPrefabs;
    //存储当前场景ai的列表
    public List<GameObject> _aiList = new List<GameObject>();
    //ai的初始点列表
    public List<Vector3> _startPointList = new List<Vector3>();

    //ai生成点,旋转参数,和临时gameobject
    private Vector3 _instantialPoint;
    private Vector3 _instantialRotation;
    private GameObject _temp;



    /// <summary>
    /// 人质生成的变量
    /// </summary>

    #region
    public GameObject _hostage;

    public float _addHostageTime = 5f;
    private float _myHostageTime;
    private Vector3[] _hostageStartPoint;

    #endregion

    /// <summary>
    /// 游戏结束的UI窗口
    /// </summary>
    public GameObject _gameOverWindows;

    /// <summary>
    /// 复活窗口
    /// </summary>
    public GameObject _resurrectionWindows;
    private bool _hasResurrect;
    public GameObject _NoNet;//广告没被正常播放的提示

    public List<GameObject> _playerPrefabs;

    private void Awake()
    {
        TGSDK.Initialize();
        TGSDK.PreloadAd();
        TGSDK.AdRewardSuccessCallback = OnAdRewardSuccess;
        TGSDK.AdRewardFailedCallback = OnAdRewardFailed;
        Instance = this;

        CreatePlayer();

        _player = GameObject.FindGameObjectWithTag(_Data._tagPlayer);
        _playerCollider = _player.GetComponent<CapsuleCollider>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
        InitialPlayer();

    }

    void Start()
    {
        
        _hasResurrect = false;
        _needToKillCount = 3;
        InstantialList();
        _instantialPoint = new Vector3(0, 0, 5);
        _instantialRotation = new Vector3(0, 180, 0);

        //初始化ai
        InstantialOneAI();
        _myHostageTime = 0f;
        _hostageStartPoint = new Vector3[] {new Vector3(-1.5f, 0, -2.5f), new Vector3(1.5f, 0, -2.5f)};
    }

    private void CreatePlayer()
    {
        Instantiate(_playerPrefabs[_Data._lastPickCharc],Vector3.zero,Quaternion.identity);
    }


    private void InitialPlayer()
    {
        _player.GetComponent<PlayerAtkDmg>().enabled = true;
        _player.GetComponent<PlayerCharacteristic>().enabled = true;
        _player.GetComponent<PlayerMovement>().enabled = true;
    }


    void Update()
    {
        if (!_isBossTime)
        {
            if (PlayerCharacteristic.Instance._isDead)
            {
                if (IsInvoking("InstantialOtherTwoAI"))
                    CancelInvoke("InstantialOtherTwoAI");
                else if (IsInvoking("AddBoss"))
                    CancelInvoke("AddBoss");
                return;
            }
            if (_aiList.Count < 3&&!IsInvoking("InstantialOtherTwoAI"))
                AddAI();
            else if (IsInvoking("InstantialOtherTwoAI") && _aiList.Count == 0)
                AddAI();
            if (_currentKillCount >= _needToKillCount)
            {
                //生成boss
                CancelInvoke();
                _needToKillCount = 5;
                AddBoss();
            }
        }
        else
        {
            _currentKillCount = 0;
        }
        

        //人质的生成逻辑
        #region
        _myHostageTime += Time.deltaTime;
        if (_myHostageTime > _addHostageTime && !PlayerCharacteristic.Instance._isDead)
        {
            //生成人质
            AddHostage(Random.Range(0, 2));
            _myHostageTime = 0f;
        }

        #endregion
    }


    /// <summary>
    /// 初始化AI
    /// </summary>
    private void InstantialOneAI()
    {
        _temp = Instantiate(_aiPrefabs[Random.Range(0, _aiPrefabs.Length)], _instantialPoint, Quaternion.Euler(_instantialRotation));
        _aiList.Add(_temp);
        _temp.GetComponent<AIAtkDmg>().StartPoint = _startPointList[0];
        _startPointList.RemoveAt(0);

        Invoke("InstantialOtherTwoAI", 30f);

        _currentKillCount = 0;
    }

    private void InstantialOtherTwoAI()
    {
        for (int i = 0; i < 2; i++)
        {
            _temp = Instantiate(_aiPrefabs[Random.Range(0, _aiPrefabs.Length)], _instantialPoint,
                Quaternion.Euler(_instantialRotation));
            _aiList.Add(_temp);
            _temp.GetComponent<AIAtkDmg>().StartPoint = _startPointList[0];
            _startPointList.RemoveAt(0);
        }

        Invoke("AddBoss", 20f);

    }

    public GameObject AddAI()
    {
        _temp = Instantiate(_aiPrefabs[Random.Range(0, _aiPrefabs.Length)], _instantialPoint,
            Quaternion.Euler(_instantialRotation));
        _aiList.Add(_temp);
        _temp.GetComponent<AIAtkDmg>().StartPoint = _startPointList[0];
        _startPointList.RemoveAt(0);
        return _temp;
    }

    private void AddBoss()
    {
        _boss.SetActive(true);
        _isBossTime = true;
    }


    private void AddHostage(int randomInt)
    {
        Instantiate(_hostage, _hostageStartPoint[randomInt], Quaternion.identity);
    }

    /// <summary>
    /// 初始化开始点的位置
    /// </summary>
    private void InstantialList()
    {
        _startPointList.Add(new Vector3(0, 0, 3.05f));
        _startPointList.Add(new Vector3(-1.3f, 0, 3.05f));        
        _startPointList.Add(new Vector3(1.3f, 0, 3.05f));
    }

    private void PlayerDead()
    {
        _playerCollider.enabled = false;
        _playerMovement.enabled = false;
        if (!_hasResurrect)
        {

            Invoke("ShowResurrectionWindows", 1f);
        }
        else
        {
            Invoke("ShowGameOverWindows", 1f);
        }
    }

    private void PlayerResurrect()
    {
        _playerCollider.enabled = true;
        _playerMovement.enabled = true;
    }

    public void RemoveAIInList(GameObject deadAI)
    {
        if (_aiList.Contains(deadAI))
            _aiList.Remove(deadAI);
        _currentKillCount++;
    }

    public void AddStartPointInList(Vector3 deadAIsStartPoint)
    {
        if (!_startPointList.Contains(deadAIsStartPoint))
            _startPointList.Add(deadAIsStartPoint);
    }

    public void KillAllPawn()
    {
        for (int i = 0; i < _aiList.Count; i++)
        {
            _aiList[i].transform.SendMessage("TakeDamage");
        }
    }

    /// <summary>
    /// 显示游戏结束窗口
    /// </summary>
    public void ShowGameOverWindows()
    {
        _gameOverWindows.SetActive(true);
    }

    /// <summary>
    /// 显示复活窗口
    /// </summary>
    private void ShowResurrectionWindows()
    {
        _resurrectionWindows.SetActive(true);
    }

    //显示广告
    public void ShowAds()
    {
        if (TGSDK.CouldShowAd(_adsID))
        {

            TGSDK.ShowAd(_adsID);
        }
        else
        {
            _NoNet.GetComponent<Text>().text = "Please check your network.";
            _NoNet.SetActive(true);
        }
    }

    //广告被正常播放，复活玩家
    public void OnAdRewardSuccess(string ret)
    {
        _resurrectionWindows.SetActive(false);
        PlayerCharacteristic.Instance.Resurrection();
        _hasResurrect = true;
    }

    //广告没被正常播放
    public void OnAdRewardFailed(string error)
    {
        _resurrectionWindows.SetActive(false);
        _gameOverWindows.SetActive(true);
    }

    /// <summary>
    /// 花费5宝石复活
    /// </summary>
    public void UseGemToResurrect()
    {
        //扣宝石，复活。
        if (_Data._gem >= 5)
        {
            _Data._gem -= 5;
            _resurrectionWindows.SetActive(false);
            PlayerCharacteristic.Instance.Resurrection();
            _hasResurrect = true;
            _Data.SaveData();
        }
        else
        {
            _NoNet.GetComponent<Text>().text = "Not enough gems.";
            _NoNet.SetActive(true);
        }

    }

}



