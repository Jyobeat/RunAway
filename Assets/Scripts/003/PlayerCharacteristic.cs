using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacteristic : MonoBehaviour
{
    //单例模式
    public static PlayerCharacteristic Instance;

    private Animator _anim;
    private Button _btnAttack;

    //当前拥有的刀的数量    
    public int _knifeCount;
    //最大能持有的刀的数量
    public int _knifeMaxCount=3;

    //UI中的生命和刀的父物体
    public Transform _healthParent;
    public Transform _knifeParent;

    //生命
    public int _health = 3;

    [HideInInspector]
    public bool _isDead = false;

    [HideInInspector] public bool _hasHostage = false;
    

    //游戏逻辑的管理器  
    private GameObject _gameLogicManager;

    private void Awake()
    {
        

    }

    void Start ()
	{
        Instance = this;
        _btnAttack = GameObject.Find("Btn_Atk").GetComponent<Button>();
        _healthParent = GameObject.Find("Health").transform;
        _knifeParent = GameObject.Find("Knife").transform;
        _gameLogicManager = GameObject.FindGameObjectWithTag(_Data._tagGameController);
        _anim = GetComponent<Animator>();


        InitialKnifeAndHealth();

	}

    /// <summary>
    /// 初始化刀和生命
    /// </summary>
    private void InitialKnifeAndHealth()
    {
        ThreeUIManager.Instance.ReflashUI(_health, _healthParent);

        if (_knifeCount > 0)
        {
            _btnAttack.interactable = true;
            ThreeUIManager.Instance.ReflashUI(_knifeCount, _knifeParent);
        }
    }

    private void PickUp()
    {
        if (_knifeCount < _knifeMaxCount)
        {
            _knifeCount++;
            _btnAttack.interactable = true;
            ThreeUIManager.Instance.ReflashUI(_knifeCount, _knifeParent);
            //todo:动画
        }
    }

    public void DecreaseHealth()
    {
        ThreeUIManager.Instance.ReflashUI(--_health, _healthParent);
        if (_health == 0)
        {
            _isDead = true;
            _anim.SetBool("Run", false);
            _anim.SetTrigger("Dead");
            _gameLogicManager.SendMessage("PlayerDead");
            TerrainMoveManager.Instance._moveSpeed = 0;
        }
    }

    public void IncreaseHealth()
    {
        if (_health < 3)
        {
            _health++;
            ThreeUIManager.Instance.ReflashUI(_health, _healthParent);
        }
    }

    //复活
    public void Resurrection()
    {
        _health = 3;
        ThreeUIManager.Instance.ReflashUI(_health, _healthParent);
        _isDead = false;
        _anim.SetTrigger("ReBorn");
        TerrainMoveManager.Instance._moveSpeed = 1.5f;
        _gameLogicManager.SendMessage("PlayerResurrect");
    }
	
}
