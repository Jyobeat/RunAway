
using UnityEngine;

using BehaviorDesigner.Runtime.Tasks;

public class Pawn_Throw :Action
{
    public Animator _ani;
    public float _waitTime = 2f;
    private float _myTime;

    public override void OnAwake()
    {
        _ani = GetComponent<Animator>();
        _myTime = 0f;
    }

    public override void OnStart()
    {
        _ani.SetTrigger("Throw");
    }

    public override TaskStatus OnUpdate()
    {
        if (_myTime < _waitTime)
        {
            _myTime += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {

            return TaskStatus.Success;
        }
    }

}
