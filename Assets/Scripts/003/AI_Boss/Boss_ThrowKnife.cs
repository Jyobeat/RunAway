using BehaviorDesigner .Runtime.Tasks;
using UnityEngine;

public class Boss_ThrowKnife : Action
{
    private Animator _anim;
    public float _waitTime=1f;
    private float _time;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _anim.SetTrigger("ThrowKnife");
        _time = 0;
    }

    public override TaskStatus OnUpdate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime)
            return TaskStatus.Running;
        
        return TaskStatus.Success;
    }

}
