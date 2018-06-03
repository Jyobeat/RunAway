using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharcManager : MonoBehaviour
{
    public List<GameObject> _playerList;

    public static CharcManager Instance;

    //储存所有人物开关的列表
    public GameObject[] _charcToggles;
    
    private CharcSelect _charcSelect;

    public GameObject _noMoneyTips;

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        _charcSelect = new CharcSelect();
        ReduceLastPickCharc();
	    ShowPriceOnCharc();
    }

    
    //在人物右边根据是否已经购买显示价格
    public void ShowPriceOnCharc()
    {
        for (int i = 0; i < _charcToggles.Length; i++)
        {
            //已购买
            if (_Data._unlockCharc[i] == 1)
            {
                _charcToggles[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                _charcToggles[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    //得到玩家当前选择的人物
    public void SetCurrentCharcSelect(int currentCharc)
    {
        _Data._lastPickCharc = currentCharc;
    }

    //购买人物，购买成功时设置人物列表转态为1
    public void BuyCharc()
    {
        if (_Data._unlockCharc[_Data._lastPickCharc] == 0)
        {
            //如果选择的角色是0-2号，扣金币
            if (_Data._lastPickCharc <= 2)
            {
                //如果钱够
                if (_Data._coin >= _Data.PriceOfCharc[_Data._lastPickCharc])
                {
                    _Data._coin -= _Data.PriceOfCharc[_Data._lastPickCharc];
                    _Data._unlockCharc[_Data._lastPickCharc] = 1;
                    ShowPriceOnCharc();
                    _charcSelect.SetOffBtnBuy();
                    _Data.SaveData();
                }
                //不够钱，出提示
                else
                {
                    _noMoneyTips.SetActive(true);
                    
                }
            }

            else if (_Data._lastPickCharc >= 3)
            {
                //如果钱够
                if (_Data._gem >= _Data.PriceOfCharc[_Data._lastPickCharc])
                {
                    _Data._gem -= _Data.PriceOfCharc[_Data._lastPickCharc];
                    _Data._unlockCharc[_Data._lastPickCharc] = 1;
                    ShowPriceOnCharc();
                    _charcSelect.SetOffBtnBuy();
                    _Data.SaveData();
                }
                //不够钱，出提示
                else
                {
                    //todo:tips充钱
                    _noMoneyTips.SetActive(true);
                }
            }

        }

    }

    //当关闭人物窗口时，判断当前选择角色是否已经购买，如果没有购买，就自动选回初始人物
    public void JudgeWhileClosing()
    {
        //若当前选择的人物是未购买的
        if (_Data._unlockCharc[_Data._lastPickCharc] == 0)
        {
            _charcToggles[_Data._lastPickCharc].GetComponent<Toggle>().isOn = false;
            _charcToggles[0].GetComponent<Toggle>().isOn = true;
            _Data._lastPickCharc = 0;
        }
        //进行一次存档
        _Data.SaveData();
    }

    //不销毁当前选择的玩家
    public void KeepThePlayer()
    {
        DontDestroyOnLoad(_playerList[_Data._lastPickCharc]);
    }

    //还原上次选择的人物
    public void ReduceLastPickCharc()
    {
        _charcToggles[_Data._lastPickCharc].GetComponent<Toggle>().isOn = true;
        JudgeWhileClosing();
    }

}
