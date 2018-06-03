using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class _Data
{
    //游戏的标签
    public static string _tagKnifePool = "KnifePool";
    public static string _tagPlayer = "Player";
    public static string _tagAI = "AI";
    public static string _tagKnifePoint = "KnifePoint";
    public static string _tagGameController = "GameController";
    public static string _tagWorldCanvas = "WorldCanvas";
    public static string _tagScoreTxt = "ScoreTxt";
    public static string _tagGameOverWindows = "GameOverWindows";
    public static string _tagCharcToggle = "CharcToggle";
    //层
    public static string _layerInvincible = "Invincible";
    public static string _layerEnemy = "Enemy";
    public static string _layerItem = "Item";
    public static string _layerWeapon = "Weapon";
    //小提示
    public static List<string> _tips = new List<string>
    {
        "The knife thrown by the monster has a 50% chance of falling on the ground!Pick it up and fight back!",
        "After successfully helping the hostage escape,you are rewarded with a health！Of course you can also use hostages to stop attacks.",
        "Keep a safe distance from monsters or you will be chased and attacked.",
        "If the monster is chasing you, you can use speed up skill to pull distance.",
        "Before the monster dash, he will prepare for a few seconds in situ.",
        "When the monster is not in a state of dash or chase, collision with it will not take damage."
    };

    //角色特性
    public static List<string> _charcSkill = new List<string>
    {
        "Skill: Null.",

        "Skill：Uncle Punk is very strong，" +
        "and health+1.",

        "Skill：Long years of exercise\n" +
        "make Thief more agile,and\n" +
        "Increase the speed up time by 1s",

        "Skill：Spy has three weapons in the beginning," +
        "and weapon ceiling +1" 
    };

    public static List<string> _tutorialTips = new List<string>
    {
        "Please drag the joystick to move.\n" +
        "Now, pick up the weapon.",

        "Please press the button to move faster.\n" +
        "You can dodge monster attack by this way",

        "Please press the button to throw a knife.\n" +
        "And it will consume a weapon.",

        "Watch out!!! The monster is gonna dash!",

        "The monster is angry at not hitting you. Now he is chasing you.\n" +
        "Please press speed up button to pull distance.",

        "Watch out !!! The monster is gonna throw knife at you!\n" +
        "Tips:The knife thrown by the monster has a 50% chance of falling on the ground!" ,

        "Now,please pick up the knife and fight back.",

        "Occasionally there will be hostages on the road. You can run to rescue her.\n"+
        "After helping the hostage,you are rewarded with a health",

        "Congratulations!You have completed the tutorial.Now enjoy the game!"

    };


    public static int[] PriceOfCharc = new int[] {0, 2000, 5000, 50};


    //游戏中要储存读取的数据：金币，钻石，最高分，上次选取的人物，已解锁的人物列表；
    public static int _coin=0;
    public static int _gem=0;
    public static int _higestScore=0;
    public static int _lastPickCharc=0;
    public static bool _isTaught = false;
    public static List<int> _unlockCharc = new List<int> {1, 0, 0, 0};//1表示解锁，0表示未解锁

    /// <summary>
    /// 把数据存到AppData类的实例中，并且返回这个实例
    /// </summary>
    /// <returns></returns>
    public static AppData SetData()
    {
        AppData appdata = new AppData();
        appdata._coin = _coin;
        appdata._gem = _gem;
        appdata._highestScore = _higestScore;
        appdata._lastPickCharc = _lastPickCharc;
        appdata._isTaught = _isTaught;

        foreach (var status in _unlockCharc)
        {
            appdata._unlockCharc.Add(status);
        }

        return appdata;
    }

    /// <summary>
    /// 读取Json文件的数据，来刷新游戏中的数据
    /// </summary>
    public static void ReflashData(AppData appData)
    {
        _coin = appData._coin;
        _gem = appData._gem;
        _higestScore = appData._highestScore;
        _lastPickCharc = appData._lastPickCharc;
        _isTaught = appData._isTaught;

        _unlockCharc.Clear();
        foreach (var data in appData._unlockCharc)
        {
            _unlockCharc.Add(data);
        }
    }


    public static void SaveData()
    {
        //创建一个AppData实例来保存现有的数据
        AppData _appData = SetData();
        //要存储的json文件的路径名
        string _filePath = Application.persistentDataPath +  "/RunGameData.Json";
        //利用JsonMapper将_appData转化为Json格式的字符串。
        string _saveJsonStr = JsonMapper.ToJson(_appData);

        //创建一个写入流将Json字符串写入到文件中。
        StreamWriter _sw = new StreamWriter(_filePath);
        _sw.Write(_saveJsonStr);

        //关闭流
        _sw.Close();

    }

    public static void LoadData()
    {
        //要读取的json文件的路径名
        string _filePath = Application.persistentDataPath + "/RunGameData.Json";
        //如果目录下存在Data.Json
        if (File.Exists(_filePath))
        {
            //创建文件读取流读取Json文件
            StreamReader _sr = new StreamReader(_filePath);
            string _jsonStr = _sr.ReadToEnd();
            //关闭流
            _sr.Close();


            AppData _appData = JsonMapper.ToObject<AppData>(_jsonStr);
            ReflashData(_appData);

        }

    }

}
