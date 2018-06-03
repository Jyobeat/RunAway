using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Pawn_MoveToStartPoint : Action
{

    private Vector3 _startPoint;
    private float _dis;
    private Animator _anim;

    public override void OnAwake()
    {
        _startPoint = transform.GetComponent<AIAtkDmg>().StartPoint;
        _anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        _anim.SetTrigger("Run");
    }

    public override TaskStatus OnUpdate()
    {
        _dis = Vector3.SqrMagnitude(transform.position-_startPoint);
        if (_dis > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPoint, 2*Time.deltaTime);
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }

}
