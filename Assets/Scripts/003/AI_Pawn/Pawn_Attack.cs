using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime .Tasks;

public class Pawn_Attack : Action
{

    public Pawn_Throw _throw;
    private Animator _ani;

    public float _waitTime = 0.5f;
    private float _mytime;

    public override void OnStart()
    {
        _ani = _throw._ani;
        _ani.speed = 2f;
        _ani.SetTrigger("Attack");
        _mytime = 0f;
    }

    public override TaskStatus OnUpdate()
    {
        if (_mytime < _waitTime)
        {
            _mytime += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }

    public override void OnEnd()
    {
        _ani.speed = 1f;
    }

}
