using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Pawn_Idle : Action
{

    private bool _isAtStartPoint;//是否在起始点
    private Vector3 _startPoint;
    //public SharedInt _seed;

    public override void OnAwake()
    {
        _startPoint = transform.GetComponent<AIAtkDmg>().StartPoint;
        transform.GetComponent<BehaviorTree>().GetVariable("seed").SetValue(Random.Range(0, 10));
        
        if (transform.position.z < _startPoint.z)
        {
            _isAtStartPoint = false;
        }
        else
        {
            _isAtStartPoint = true;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Mathf.Abs( transform.position.z - _startPoint.z)>0.1f)
        {
            _isAtStartPoint = false;
        }
        else
        {
            _isAtStartPoint = true;
        }


        if (_isAtStartPoint)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            return TaskStatus.Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPoint,
                2.5f*Time.deltaTime);
            return TaskStatus.Running;
        }
    }

    public override void OnFixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 15f * Time.deltaTime);
    }

}
