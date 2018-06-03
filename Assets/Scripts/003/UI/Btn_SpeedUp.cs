using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Btn_SpeedUp : MonoBehaviour
{
    private GameObject _player;
    private EventTrigger _trigger;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(_Data._tagPlayer);

        UnityAction<BaseEventData> _onPress = _player.GetComponent<PlayerMovement>().OnPress;
        UnityAction<BaseEventData> _onLoose = _player.GetComponent<PlayerMovement>().OnLoose;

        _trigger = GetComponent<EventTrigger>();
        if (_trigger.triggers.Count == 0)
        {
            //添加事件类型列表
            _trigger.triggers = new List<EventTrigger.Entry>();
        }

        AddTriggerListener(EventTriggerType.PointerDown, _onPress);
        AddTriggerListener(EventTriggerType.PointerUp, _onLoose);

    }

    private void AddTriggerListener(EventTriggerType type,UnityAction<BaseEventData> action)
    {
        //定义一个要绑定的事件
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //这个事件的类型
        entry.eventID = type;

        //设置回调函数
        entry.callback.AddListener(action);
        //把entry加入事件列表
        _trigger.triggers.Add(entry);
    }


}
