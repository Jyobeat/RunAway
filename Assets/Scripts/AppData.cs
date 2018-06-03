using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    //金币钻石
    public int _coin;
    public int _gem;

    //最高分
    public int _highestScore;

    //上次选择的人物
    public int _lastPickCharc;

    public bool _isTaught;

    //已经解锁的人物数组
    public List<int> _unlockCharc = new List<int>();
}
