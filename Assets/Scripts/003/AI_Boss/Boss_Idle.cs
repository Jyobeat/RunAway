using BehaviorDesigner .Runtime;
using BehaviorDesigner .Runtime.Tasks;
using UnityEngine;

public class Boss_Idle : Action
{

    private Vector3 _startPos;
    private Vector3 _targetRotation = new Vector3(0, 180, 0);
    private BehaviorTree _selfTree;

    public override void OnAwake()
    {
        _startPos = GetComponent<BossAtkAndDmg>().StartPoint;
        _selfTree = GetComponent<BehaviorTree>();
        _selfTree.SetVariableValue("seed", Random.Range(0, 10));
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - _startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPos,
                0.9f*Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_targetRotation), 10*Time.deltaTime);
            return TaskStatus.Running;
        }
        else
        {
            transform.rotation = Quaternion.Euler(_targetRotation);
            return TaskStatus.Success;
        }
    }
}
