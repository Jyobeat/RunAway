using BehaviorDesigner .Runtime.Tasks;
using UnityEngine;

public class Boss_Call : Action
{
    private Animator _anim;

    public float _waitTime=1f;
    private float _time;

    private int _pawnTotalCount = 3;
    private int _pawnCurrentCount = 0;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }


    public override void OnStart()
    {
        
        _time = 0;
        _pawnCurrentCount++;
    }

    public override TaskStatus OnUpdate()
    {
        if (_pawnCurrentCount < _pawnTotalCount)
        {
            _time += Time.deltaTime;
            if (_time < _waitTime)
                return TaskStatus.Running;
            _anim.SetTrigger("Call");
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }


}
