using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Pawn_Chase : Action {

    public float _chaseTime = 2f;
    public float _chaseSpeed = 2.25f;
    public float _attachDis = 0.65f;
    //当前追逐的僵尸数量。
    public SharedInt _currentChasing;
    //随机种子
    //public SharedInt _seed;

    private float _myTime;

    //public Pawn_WithinSight _pawnWithin;

    private SharedTransform _target;

    public override void OnStart()
    {
        _myTime = 0f;
        //_target = _pawnWithin._player;
        _target = GameObject.FindGameObjectWithTag(_Data._tagPlayer).transform;
        _currentChasing.Value++;
        //_seed.Value = (int)Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        if(_myTime<_chaseTime)
        {
            _myTime += Time.deltaTime;

            if (Vector3.SqrMagnitude(transform.position - _target.Value.position) < _attachDis)
            {
                return TaskStatus.Success;
            }

            //旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.Value.position - transform.position), 20f * Time.deltaTime);
            //移动
            transform.position = Vector3.MoveTowards(transform.position, _target.Value.position, _chaseSpeed * Time.deltaTime);
            
            //如果追到，返回成功
            
            return TaskStatus.Running;
        }
        else
        {        
            return TaskStatus.Failure;
        }
    }

    public override void OnEnd()
    {
        _currentChasing.Value--;
    }

}
